/* =========================================================================
   清查 / 清理重複帳號（Members.Account 重複）腳本
   原則：同一 Account 留下一筆（保留 Id 最小者），其餘重複者只有在
         「完全沒有任何關聯資料」時才允許刪除。
   使用方式：
     1) 先執行【STEP 1】確認重複帳號清單
     2) 再執行【STEP 2】確認「可安全刪除」與「有關聯不可刪除」的清單
     3) 確認無誤後，取消 STEP 3 的註解再執行刪除
   ========================================================================= */

-- ============ STEP 1：列出所有重複帳號 ============
SELECT Account, COUNT(*) AS DupCount
FROM dbo.Members
GROUP BY Account
HAVING COUNT(*) > 1
ORDER BY Account;


-- ============ STEP 2：標記每筆重複帳號是否有關聯，並決定要保留/刪除的那一筆 ============
;WITH Dup AS (
    SELECT Id, Account,
           ROW_NUMBER() OVER (PARTITION BY Account ORDER BY Id) AS rn
    FROM dbo.Members
    WHERE Account IN (
        SELECT Account FROM dbo.Members GROUP BY Account HAVING COUNT(*) > 1
    )
),
Refs AS (
    SELECT m.Id,
           (SELECT COUNT(*) FROM dbo.News               WHERE MemberId = m.Id) +
           (SELECT COUNT(*) FROM dbo.CasePoprocs        WHERE MemberId = m.Id) +
           (SELECT COUNT(*) FROM dbo.CasePoprocLogs     WHERE MemberId = m.Id) +
           (SELECT COUNT(*) FROM dbo.CaseTrafficPoprocs WHERE MemberId = m.Id) +
           (SELECT COUNT(*) FROM dbo.CaseTrafficPoprocLogs WHERE MemberId = m.Id) +
           (SELECT COUNT(*) FROM dbo.Trafficdisdatas    WHERE MemberId = m.Id) +
           (SELECT COUNT(*) FROM dbo.TrafficDepartmentdetails WHERE MemberId = m.Id) +
           (SELECT COUNT(*) FROM dbo.RoleMembers        WHERE Member_Id = m.Id)
           AS RefCount
    FROM dbo.Members m
    JOIN Dup d ON d.Id = m.Id
)
SELECT d.Account, d.Id, d.rn AS KeepIfRn1, r.RefCount,
       CASE WHEN d.rn = 1 THEN N'保留(同帳號最早建立的一筆)'
            WHEN r.RefCount = 0 THEN N'可安全刪除(無任何關聯)'
            ELSE N'⚠ 有關聯，無法自動刪除，需人工確認後合併或保留'
       END AS Action
FROM Dup d
JOIN Refs r ON r.Id = d.Id
ORDER BY d.Account, d.rn;


-- ============ STEP 3：實際刪除（請先確認 STEP 2 結果無誤後，取消下方註解執行）============
/*
BEGIN TRANSACTION;

;WITH Dup AS (
    SELECT Id, Account,
           ROW_NUMBER() OVER (PARTITION BY Account ORDER BY Id) AS rn
    FROM dbo.Members
    WHERE Account IN (
        SELECT Account FROM dbo.Members GROUP BY Account HAVING COUNT(*) > 1
    )
),
Refs AS (
    SELECT m.Id,
           (SELECT COUNT(*) FROM dbo.News               WHERE MemberId = m.Id) +
           (SELECT COUNT(*) FROM dbo.CasePoprocs        WHERE MemberId = m.Id) +
           (SELECT COUNT(*) FROM dbo.CasePoprocLogs     WHERE MemberId = m.Id) +
           (SELECT COUNT(*) FROM dbo.CaseTrafficPoprocs WHERE MemberId = m.Id) +
           (SELECT COUNT(*) FROM dbo.CaseTrafficPoprocLogs WHERE MemberId = m.Id) +
           (SELECT COUNT(*) FROM dbo.Trafficdisdatas    WHERE MemberId = m.Id) +
           (SELECT COUNT(*) FROM dbo.TrafficDepartmentdetails WHERE MemberId = m.Id) +
           (SELECT COUNT(*) FROM dbo.RoleMembers        WHERE Member_Id = m.Id)
           AS RefCount
    FROM dbo.Members m
    JOIN Dup d ON d.Id = m.Id
)
DELETE FROM dbo.Members
WHERE Id IN (
    SELECT d.Id
    FROM Dup d
    JOIN Refs r ON r.Id = d.Id
    WHERE d.rn > 1        -- 不是該帳號最早的一筆
      AND r.RefCount = 0  -- 且完全沒有任何關聯資料
);

-- 確認筆數無誤後再 COMMIT，若不對請 ROLLBACK
-- COMMIT TRANSACTION;
-- ROLLBACK TRANSACTION;
*/
