# 網站健康檢查程式 — 交通違規檢舉信箱

定期檢查交通違規檢舉信箱網站是否正常運作的 Python 腳本。

檢查目標：

```
https://tr.tnpd.gov.tw/TrafficMailbox/Create?checkRead=agree&id=4b0a8176-a08d-60a4-b25f-cbb95e009897
```

## 檔案說明

| 檔案 | 說明 |
| --- | --- |
| `check_website.py` | 主要檢查腳本，每次執行會檢查一次網站狀態 |
| `Register-ScheduledTask.ps1` | 建立/移除 Windows 工作排程的輔助腳本 |
| `logs\check-website.log` | 異常記錄檔（僅在偵測到異常時才會建立與寫入） |

## 判斷標準

同時符合以下兩點才視為「正常」：

1. HTTP 狀態碼為 `200`
2. 回應內容（HTML）包含關鍵字「**交通檢舉信箱**」

只要其中一項不符合，或連線失敗／逾時，即視為「異常」。

## 使用方式

### 手動執行一次

```bash
python check_website.py
```

- 網站正常：僅在終端機印出結果，不會寫入任何檔案
- 網站異常：印出異常原因，並寫入 `logs\check-website.log`

### 建立定期排程（每 15 分鐘執行一次）

需以系統管理員身分開啟 PowerShell：

```powershell
.\Register-ScheduledTask.ps1
```

移除排程：

```powershell
.\Register-ScheduledTask.ps1 -Unregister
```

排程工作名稱為 `CheckTrafficMailboxWebsite`，可在工作排程器（Task Scheduler）中查看與管理。

> 需先安裝 Python，並確認 `python.exe` 已加入系統 PATH，`Register-ScheduledTask.ps1` 才能正確排程執行 `check_website.py`。


