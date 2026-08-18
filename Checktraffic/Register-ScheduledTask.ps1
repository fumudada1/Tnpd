# 建立/移除「交通違規檢舉信箱網站健康檢查」的 Windows 工作排程
# 用法：
#   註冊排程（需以系統管理員身分執行）： .\Register-ScheduledTask.ps1
#   移除排程：                          .\Register-ScheduledTask.ps1 -Unregister

param(
    [switch]$Unregister
)

$TaskName = 'CheckTrafficMailboxWebsite'
$ScriptPath = Join-Path $PSScriptRoot 'check_website.py'

if ($Unregister) {
    Unregister-ScheduledTask -TaskName $TaskName -Confirm:$false -ErrorAction Stop
    Write-Host "已移除排程工作：$TaskName"
    return
}

$action = New-ScheduledTaskAction -Execute 'python.exe' `
    -Argument "`"$ScriptPath`""

$trigger = New-ScheduledTaskTrigger -Once -At (Get-Date) `
    -RepetitionInterval (New-TimeSpan -Minutes 15) `
    -RepetitionDuration ([TimeSpan]::MaxValue)

$settings = New-ScheduledTaskSettingsSet -StartWhenAvailable -DontStopOnIdleEnd

Register-ScheduledTask -TaskName $TaskName -Action $action -Trigger $trigger -Settings $settings `
    -Description '每 15 分鐘檢查交通違規檢舉信箱網站是否正常' -Force | Out-Null

Write-Host "已建立排程工作：$TaskName（每 15 分鐘執行一次）"
