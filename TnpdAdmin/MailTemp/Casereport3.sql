WITH CaseProcessingDays AS (
    SELECT 
        c.Id AS CaseId,
        c.CaseID AS CaseNumber,  -- 改名為CaseNumber避免重複
        c.Subject,
        cp.UnitId,
        -- 使用父單位ID來對應分局
        ISNULL(u.ParentId, u.Id) AS BureauId,
        CASE 
            WHEN cp.EndDateTime IS NOT NULL THEN
                -- 已完成案件：結束時間 - 開始時間 - 期間的假日數
                CAST(DATEDIFF(DAY, c.InitDate, cp.EndDateTime) AS DECIMAL(10,2)) - 
                (SELECT COUNT(DISTINCT h.InitDate) 
                 FROM Holidays h 
                 WHERE h.IsHoliday = 1 
                 AND h.InitDate >= c.InitDate 
                 AND h.InitDate <= cp.EndDateTime)
            ELSE
                -- 未完成案件：今天 - 開始時間 - 期間的假日數
                CAST(DATEDIFF(DAY, c.InitDate, GETDATE()) AS DECIMAL(10,2)) - 
                (SELECT COUNT(DISTINCT h.InitDate) 
                 FROM Holidays h 
                 WHERE h.IsHoliday = 1 
                 AND h.InitDate >= c.InitDate 
                 AND h.InitDate <= GETDATE())
        END AS ProcessingDays
    FROM 
        Cases c
    JOIN 
        CasePoprocs cp ON c.Id = cp.CaseId
    LEFT JOIN
        Units u ON cp.UnitId = u.Id
    WHERE
        c.InitDate IS NOT NULL AND
        cp.UnitId IS NOT NULL AND
        -- 限制案件創建日期在2025/1/1至2025/2/1之間
        c.InitDate >= '2025/1/1' AND 
        c.InitDate <= '2025/2/1'
)

SELECT 
    b.Id AS BureauId,
    b.Subject AS BureauName,
    COUNT(cpd.CaseId) AS TotalCases,
    ROUND(AVG(cpd.ProcessingDays), 2) AS AvgProcessingDays
    
FROM 
    CaseProcessingDays cpd
JOIN 
    Units b ON cpd.BureauId = b.Id
-- 確保只包含分局層級的單位
WHERE 
    b.ParentId IS NULL  -- 假設頂層單位(無上級單位)為分局
GROUP BY 
    b.Id, b.Subject
HAVING 
    COUNT(cpd.CaseId) > 0  -- 只顯示有案件的分局
ORDER BY 
    b.Subject;