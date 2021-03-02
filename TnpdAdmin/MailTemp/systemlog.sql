SELECT          COUNT(*) AS Total, SystemLogs.Poster, (Members.Account+ '' +Members.Name) as Name, Units.Subject as Unit, Units_1.Subject AS Unit1
FROM              SystemLogs INNER JOIN
                            Members ON SystemLogs.Poster = Members.Account INNER JOIN
                            Units ON Members.UnitId = Units.Id INNER JOIN
                            Units AS Units_1 ON Units.ParentId = Units_1.Id
where SystemLogs.InitDate>='2019/1/1' and SystemLogs.InitDate<='2019/3/1'   and SystemLogs.Subject like '%網站節點%'
GROUP BY   SystemLogs.Poster, (Members.Account+ '' +Members.Name), Units.Subject, Units_1.Subject
ORDER BY   SystemLogs.Poster