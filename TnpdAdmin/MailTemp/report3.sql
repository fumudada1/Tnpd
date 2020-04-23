SELECT          CaseTraffics.itemno, COUNT(*) AS 受理件數  into #t1
FROM              CaseTraffics INNER JOIN
                            CaseTrafficPoprocs ON CaseTraffics.Id = CaseTrafficPoprocs.CaseId
							where CaseTrafficPoprocs.EndDateTime is not null  and CaseTraffics.InitDate between '2019/1/1' and '2019/3/1'
GROUP BY   CaseTraffics.itemno


SELECT          CaseTraffics.itemno, COUNT(*) AS 舉發件數 into #t2
FROM              CaseTraffics INNER JOIN
                            CaseTrafficPoprocs ON CaseTraffics.Id = CaseTrafficPoprocs.CaseId
							where CaseTrafficPoprocs.EndDateTime is not null and CaseTrafficPoprocs.PoprocsType=2   and CaseTraffics.InitDate between '2019/1/1' and '2019/3/1'
GROUP BY   CaseTraffics.itemno

SELECT          CaseTraffics.itemno, COUNT(*) AS 不予舉發件數合計 into #t3
FROM              CaseTraffics INNER JOIN
                            CaseTrafficPoprocs ON CaseTraffics.Id = CaseTrafficPoprocs.CaseId
							where CaseTrafficPoprocs.EndDateTime is not null and CaseTrafficPoprocs.PoprocsType=1  and CaseTraffics.InitDate between '2019/1/1' and '2019/3/1'
GROUP BY   CaseTraffics.itemno

SELECT          CaseTraffics.itemno, COUNT(*) AS 逾7日之檢舉 into #t4
FROM              CaseTraffics INNER JOIN
                            CaseTrafficPoprocs ON CaseTraffics.Id = CaseTrafficPoprocs.CaseId
							where CaseTrafficPoprocs.EndDateTime is not null and CaseTrafficPoprocs.ViolationsRejectclassId=2 and CaseTrafficPoprocs.PoprocsType=1  and CaseTraffics.InitDate between '2019/1/1' and '2019/3/1'
GROUP BY   CaseTraffics.itemno

SELECT          CaseTraffics.itemno, COUNT(*) AS 時間未明 into #t5
FROM              CaseTraffics INNER JOIN
                            CaseTrafficPoprocs ON CaseTraffics.Id = CaseTrafficPoprocs.CaseId
							where CaseTrafficPoprocs.EndDateTime is not null and CaseTrafficPoprocs.ViolationsRejectclassId=3 and CaseTrafficPoprocs.PoprocsType=1  and CaseTraffics.InitDate between '2019/1/1' and '2019/3/1'
GROUP BY   CaseTraffics.itemno

SELECT          CaseTraffics.itemno, COUNT(*) AS 地點不明 into #t6
FROM              CaseTraffics INNER JOIN
                            CaseTrafficPoprocs ON CaseTraffics.Id = CaseTrafficPoprocs.CaseId
							where CaseTrafficPoprocs.EndDateTime is not null and CaseTrafficPoprocs.ViolationsRejectclassId=4 and CaseTrafficPoprocs.PoprocsType=1  and CaseTraffics.InitDate between '2019/1/1' and '2019/3/1'
GROUP BY   CaseTraffics.itemno

SELECT          CaseTraffics.itemno, COUNT(*) AS 證據資料顯示行為態樣未符違規構成要件 into #t7
FROM              CaseTraffics INNER JOIN
                            CaseTrafficPoprocs ON CaseTraffics.Id = CaseTrafficPoprocs.CaseId
							where CaseTrafficPoprocs.EndDateTime is not null and CaseTrafficPoprocs.ViolationsRejectclassId=5 and CaseTrafficPoprocs.PoprocsType=1  and CaseTraffics.InitDate between '2019/1/1' and '2019/3/1'
GROUP BY   CaseTraffics.itemno

SELECT          CaseTraffics.itemno, COUNT(*) AS 未檢附證據資料或證據不足 into #t8
FROM              CaseTraffics INNER JOIN
                            CaseTrafficPoprocs ON CaseTraffics.Id = CaseTrafficPoprocs.CaseId
							where CaseTrafficPoprocs.EndDateTime is not null and CaseTrafficPoprocs.ViolationsRejectclassId=6 and CaseTrafficPoprocs.PoprocsType=1  and CaseTraffics.InitDate between '2019/1/1' and '2019/3/1'
GROUP BY   CaseTraffics.itemno

SELECT          CaseTraffics.itemno, COUNT(*) AS 同一案件重複檢舉 into #t9
FROM              CaseTraffics INNER JOIN
                            CaseTrafficPoprocs ON CaseTraffics.Id = CaseTrafficPoprocs.CaseId
							where CaseTrafficPoprocs.EndDateTime is not null and CaseTrafficPoprocs.ViolationsRejectclassId=7 and CaseTrafficPoprocs.PoprocsType=1  and CaseTraffics.InitDate between '2019/1/1' and '2019/3/1'
GROUP BY   CaseTraffics.itemno

SELECT          CaseTraffics.itemno, COUNT(*) AS 輕微違規符合法令規定要件 into #t10
FROM              CaseTraffics INNER JOIN
                            CaseTrafficPoprocs ON CaseTraffics.Id = CaseTrafficPoprocs.CaseId
							where CaseTrafficPoprocs.EndDateTime is not null and CaseTrafficPoprocs.ViolationsRejectclassId=8 and CaseTrafficPoprocs.PoprocsType=1  and CaseTraffics.InitDate between '2019/1/1' and '2019/3/1'
GROUP BY   CaseTraffics.itemno

SELECT          CaseTraffics.itemno, COUNT(*) AS 偽造車牌道路工程設施爭議或其他 into #t11
FROM              CaseTraffics INNER JOIN
                            CaseTrafficPoprocs ON CaseTraffics.Id = CaseTrafficPoprocs.CaseId
							where CaseTrafficPoprocs.EndDateTime is not null and CaseTrafficPoprocs.ViolationsRejectclassId=9 and CaseTrafficPoprocs.PoprocsType=1  and CaseTraffics.InitDate between '2019/1/1' and '2019/3/1'
GROUP BY   CaseTraffics.itemno

SELECT          CaseTraffics.itemno, COUNT(*) AS 檔案壓縮致影像模糊無法辨識 into #t12
FROM              CaseTraffics INNER JOIN
                            CaseTrafficPoprocs ON CaseTraffics.Id = CaseTrafficPoprocs.CaseId
							where CaseTrafficPoprocs.EndDateTime is not null and CaseTrafficPoprocs.ViolationsRejectclassId=10 and CaseTrafficPoprocs.PoprocsType=1  and CaseTraffics.InitDate between '2019/1/1' and '2019/3/1'
GROUP BY   CaseTraffics.itemno

SELECT          CaseTraffics.itemno, COUNT(*) AS 非本轄移轉國道公路警察局 into #t13
FROM              CaseTraffics INNER JOIN
                            CaseTrafficPoprocs ON CaseTraffics.Id = CaseTrafficPoprocs.CaseId
							where CaseTrafficPoprocs.EndDateTime is not null and CaseTrafficPoprocs.ViolationsRejectclassId=11 and CaseTrafficPoprocs.PoprocsType=1  and CaseTraffics.InitDate between '2019/1/1' and '2019/3/1'
GROUP BY   CaseTraffics.itemno

SELECT          CaseTraffics.itemno, COUNT(*) AS 其他 into #t14
FROM              CaseTraffics INNER JOIN
                            CaseTrafficPoprocs ON CaseTraffics.Id = CaseTrafficPoprocs.CaseId
							where CaseTrafficPoprocs.EndDateTime is not null and CaseTrafficPoprocs.ViolationsRejectclassId=13 and CaseTrafficPoprocs.PoprocsType=1  and CaseTraffics.InitDate between '2019/1/1' and '2019/3/1'
GROUP BY   CaseTraffics.itemno

SELECT          CaseTraffics.itemno, COUNT(*) AS 非業管函移其他機關 into #t15
FROM              CaseTraffics INNER JOIN
                            CaseTrafficPoprocs ON CaseTraffics.Id = CaseTrafficPoprocs.CaseId
							where CaseTrafficPoprocs.EndDateTime is not null and CaseTrafficPoprocs.ViolationsRejectclassId=14 and CaseTrafficPoprocs.PoprocsType=1  and CaseTraffics.InitDate between '2019/1/1' and '2019/3/1'
GROUP BY   CaseTraffics.itemno

SELECT          CaseTraffics.itemno, COUNT(*) AS 姓名與身份證號不符無法查證檢舉人身份 into #t16
FROM              CaseTraffics INNER JOIN
                            CaseTrafficPoprocs ON CaseTraffics.Id = CaseTrafficPoprocs.CaseId
							where CaseTrafficPoprocs.EndDateTime is not null and CaseTrafficPoprocs.ViolationsRejectclassId=15 and CaseTrafficPoprocs.PoprocsType=1  and CaseTraffics.InitDate between '2019/1/1' and '2019/3/1'
GROUP BY   CaseTraffics.itemno

SELECT        ROW_NUMBER() OVER(order by #t1.itemno desc) AS RowNumber, #t1.itemno,#t1.受理件數,  ISNULL(#t2.舉發件數,0) as 舉發件數, ISNULL(#t3.不予舉發件數合計,0) as 不予舉發件數合計, ISNULL(#t4.逾7日之檢舉,0) as 逾7日之檢舉, ISNULL(#t5.時間未明,0) as 時間未明
				,ISNULL( #t6.地點不明,0) as 地點不明, 
                            ISNULL(#t7.證據資料顯示行為態樣未符違規構成要件,0) as 證據資料顯示行為態樣未符違規構成要件, ISNULL(#t8.未檢附證據資料或證據不足,0) as 未檢附證據資料或證據不足, ISNULL(#t9.同一案件重複檢舉,0) as 同一案件重複檢舉, 
                            ISNULL(#t10.輕微違規符合法令規定要件,0) as 輕微違規符合法令規定要件, ISNULL(#t11.偽造車牌道路工程設施爭議或其他,0) as 偽造車牌道路工程設施爭議或其他, ISNULL(#t12.檔案壓縮致影像模糊無法辨識,0) as 檔案壓縮致影像模糊無法辨識, 
                            ISNULL(#t13.非本轄移轉國道公路警察局,0) as 非本轄移轉國道公路警察局, ISNULL(#t15.非業管函移其他機關,0) as 非業管函移其他機關, 
                           ISNULL( #t16.姓名與身份證號不符無法查證檢舉人身份,0) as 姓名與身份證號不符無法查證檢舉人身份, ISNULL(#t14.其他,0) as 其他
FROM              #t1 LEFT OUTER JOIN
                            #t16 ON #t1.itemno = #t16.itemno LEFT OUTER JOIN
                            #t15 ON #t1.itemno = #t15.itemno LEFT OUTER JOIN
                            #t14 ON #t1.itemno = #t14.itemno LEFT OUTER JOIN
                            #t13 ON #t1.itemno = #t13.itemno LEFT OUTER JOIN
                            #t12 ON #t1.itemno = #t12.itemno LEFT OUTER JOIN
                            #t11 ON #t1.itemno = #t11.itemno LEFT OUTER JOIN
                            #t10 ON #t1.itemno = #t10.itemno LEFT OUTER JOIN
                            #t9 ON #t1.itemno = #t9.itemno LEFT OUTER JOIN
                            #t8 ON #t1.itemno = #t8.itemno LEFT OUTER JOIN
                            #t7 ON #t1.itemno = #t7.itemno LEFT OUTER JOIN
                            #t6 ON #t1.itemno = #t6.itemno LEFT OUTER JOIN
                            #t5 ON #t1.itemno = #t5.itemno LEFT OUTER JOIN
                            #t4 ON #t1.itemno = #t4.itemno LEFT OUTER JOIN
                            #t3 ON #t1.itemno = #t3.itemno LEFT OUTER JOIN
                            #t2 ON #t1.itemno = #t2.itemno
							
