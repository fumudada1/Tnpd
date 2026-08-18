#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""交通違規檢舉信箱網站健康檢查腳本
正常判斷標準：HTTP 狀態碼 200 且回應內容包含指定關鍵字
"""

import sys
import urllib.request
import urllib.error
from datetime import datetime
from pathlib import Path

# 統一將標準輸出設為 UTF-8，避免在不同終端機（cmd、PowerShell、Git Bash）下中文顯示成亂碼
if hasattr(sys.stdout, "reconfigure"):
    sys.stdout.reconfigure(encoding="utf-8")

TARGET_URL = (
    "https://tr.tnpd.gov.tw/TrafficMailbox/Create"
    "?checkRead=agree&id=4b0a8176-a08d-60a4-b25f-cbb95e009897"
)
EXPECTED_KEYWORD = "交通檢舉信箱"
TIMEOUT_SECONDS = 15
LOG_DIR = Path(__file__).resolve().parent / "logs"
LOG_FILE = LOG_DIR / "check-website.log"


def notify(message: str) -> None:
    # TODO: 之後在此串接實際通知機制（例如 email、Line Notify 等）
    pass


def write_failure_log(reason: str) -> None:
    LOG_DIR.mkdir(parents=True, exist_ok=True)

    timestamp = datetime.now().strftime("%Y-%m-%d %H:%M:%S")
    line = f"[{timestamp}] {reason}"

    with open(LOG_FILE, "a", encoding="utf-8") as f:
        f.write(line + "\n")

    notify(line)


def main() -> None:
    failure_reason = None

    try:
        request = urllib.request.Request(TARGET_URL, headers={"User-Agent": "Mozilla/5.0"})
        with urllib.request.urlopen(request, timeout=TIMEOUT_SECONDS) as response:
            status_code = response.status
            content = response.read().decode("utf-8", errors="replace")

        if status_code != 200:
            failure_reason = f"異常：HTTP 狀態碼為 {status_code}（預期 200）"
        elif EXPECTED_KEYWORD not in content:
            failure_reason = f"異常：回應內容未包含關鍵字「{EXPECTED_KEYWORD}」"
    except urllib.error.HTTPError as e:
        failure_reason = f"異常：HTTP 狀態碼為 {e.code}（預期 200）"
    except Exception as e:
        failure_reason = f"異常：連線失敗或逾時 - {e}"

    if failure_reason:
        write_failure_log(failure_reason)
        print(failure_reason)
    else:
        print(f"正常：網站回應狀態碼 200 且內容包含關鍵字「{EXPECTED_KEYWORD}」")


if __name__ == "__main__":
    main()
