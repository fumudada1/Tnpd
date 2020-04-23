SELECT          CaseTraffics.itemno, COUNT(*) AS ���z���  into #t1
FROM              CaseTraffics INNER JOIN
                            CaseTrafficPoprocs ON CaseTraffics.Id = CaseTrafficPoprocs.CaseId
							where CaseTrafficPoprocs.EndDateTime is not null  and CaseTraffics.InitDate between '2019/1/1' and '2019/3/1'
GROUP BY   CaseTraffics.itemno


SELECT          CaseTraffics.itemno, COUNT(*) AS �|�o��� into #t2
FROM              CaseTraffics INNER JOIN
                            CaseTrafficPoprocs ON CaseTraffics.Id = CaseTrafficPoprocs.CaseId
							where CaseTrafficPoprocs.EndDateTime is not null and CaseTrafficPoprocs.PoprocsType=2   and CaseTraffics.InitDate between '2019/1/1' and '2019/3/1'
GROUP BY   CaseTraffics.itemno

SELECT          CaseTraffics.itemno, COUNT(*) AS �����|�o��ƦX�p into #t3
FROM              CaseTraffics INNER JOIN
                            CaseTrafficPoprocs ON CaseTraffics.Id = CaseTrafficPoprocs.CaseId
							where CaseTrafficPoprocs.EndDateTime is not null and CaseTrafficPoprocs.PoprocsType=1  and CaseTraffics.InitDate between '2019/1/1' and '2019/3/1'
GROUP BY   CaseTraffics.itemno

SELECT          CaseTraffics.itemno, COUNT(*) AS �O7�餧���| into #t4
FROM              CaseTraffics INNER JOIN
                            CaseTrafficPoprocs ON CaseTraffics.Id = CaseTrafficPoprocs.CaseId
							where CaseTrafficPoprocs.EndDateTime is not null and CaseTrafficPoprocs.ViolationsRejectclassId=2 and CaseTrafficPoprocs.PoprocsType=1  and CaseTraffics.InitDate between '2019/1/1' and '2019/3/1'
GROUP BY   CaseTraffics.itemno

SELECT          CaseTraffics.itemno, COUNT(*) AS �ɶ����� into #t5
FROM              CaseTraffics INNER JOIN
                            CaseTrafficPoprocs ON CaseTraffics.Id = CaseTrafficPoprocs.CaseId
							where CaseTrafficPoprocs.EndDateTime is not null and CaseTrafficPoprocs.ViolationsRejectclassId=3 and CaseTrafficPoprocs.PoprocsType=1  and CaseTraffics.InitDate between '2019/1/1' and '2019/3/1'
GROUP BY   CaseTraffics.itemno

SELECT          CaseTraffics.itemno, COUNT(*) AS �a�I���� into #t6
FROM              CaseTraffics INNER JOIN
                            CaseTrafficPoprocs ON CaseTraffics.Id = CaseTrafficPoprocs.CaseId
							where CaseTrafficPoprocs.EndDateTime is not null and CaseTrafficPoprocs.ViolationsRejectclassId=4 and CaseTrafficPoprocs.PoprocsType=1  and CaseTraffics.InitDate between '2019/1/1' and '2019/3/1'
GROUP BY   CaseTraffics.itemno

SELECT          CaseTraffics.itemno, COUNT(*) AS �Ҿڸ����ܦ欰�A�˥��ŹH�W�c���n�� into #t7
FROM              CaseTraffics INNER JOIN
                            CaseTrafficPoprocs ON CaseTraffics.Id = CaseTrafficPoprocs.CaseId
							where CaseTrafficPoprocs.EndDateTime is not null and CaseTrafficPoprocs.ViolationsRejectclassId=5 and CaseTrafficPoprocs.PoprocsType=1  and CaseTraffics.InitDate between '2019/1/1' and '2019/3/1'
GROUP BY   CaseTraffics.itemno

SELECT          CaseTraffics.itemno, COUNT(*) AS ���˪��Ҿڸ�Ʃ��Ҿڤ��� into #t8
FROM              CaseTraffics INNER JOIN
                            CaseTrafficPoprocs ON CaseTraffics.Id = CaseTrafficPoprocs.CaseId
							where CaseTrafficPoprocs.EndDateTime is not null and CaseTrafficPoprocs.ViolationsRejectclassId=6 and CaseTrafficPoprocs.PoprocsType=1  and CaseTraffics.InitDate between '2019/1/1' and '2019/3/1'
GROUP BY   CaseTraffics.itemno

SELECT          CaseTraffics.itemno, COUNT(*) AS �P�@�ץ󭫽����| into #t9
FROM              CaseTraffics INNER JOIN
                            CaseTrafficPoprocs ON CaseTraffics.Id = CaseTrafficPoprocs.CaseId
							where CaseTrafficPoprocs.EndDateTime is not null and CaseTrafficPoprocs.ViolationsRejectclassId=7 and CaseTrafficPoprocs.PoprocsType=1  and CaseTraffics.InitDate between '2019/1/1' and '2019/3/1'
GROUP BY   CaseTraffics.itemno

SELECT          CaseTraffics.itemno, COUNT(*) AS ���L�H�W�ŦX�k�O�W�w�n�� into #t10
FROM              CaseTraffics INNER JOIN
                            CaseTrafficPoprocs ON CaseTraffics.Id = CaseTrafficPoprocs.CaseId
							where CaseTrafficPoprocs.EndDateTime is not null and CaseTrafficPoprocs.ViolationsRejectclassId=8 and CaseTrafficPoprocs.PoprocsType=1  and CaseTraffics.InitDate between '2019/1/1' and '2019/3/1'
GROUP BY   CaseTraffics.itemno

SELECT          CaseTraffics.itemno, COUNT(*) AS ���y���P�D���u�{�]�I��ĳ�Ψ�L into #t11
FROM              CaseTraffics INNER JOIN
                            CaseTrafficPoprocs ON CaseTraffics.Id = CaseTrafficPoprocs.CaseId
							where CaseTrafficPoprocs.EndDateTime is not null and CaseTrafficPoprocs.ViolationsRejectclassId=9 and CaseTrafficPoprocs.PoprocsType=1  and CaseTraffics.InitDate between '2019/1/1' and '2019/3/1'
GROUP BY   CaseTraffics.itemno

SELECT          CaseTraffics.itemno, COUNT(*) AS �ɮ����Y�P�v���ҽk�L�k���� into #t12
FROM              CaseTraffics INNER JOIN
                            CaseTrafficPoprocs ON CaseTraffics.Id = CaseTrafficPoprocs.CaseId
							where CaseTrafficPoprocs.EndDateTime is not null and CaseTrafficPoprocs.ViolationsRejectclassId=10 and CaseTrafficPoprocs.PoprocsType=1  and CaseTraffics.InitDate between '2019/1/1' and '2019/3/1'
GROUP BY   CaseTraffics.itemno

SELECT          CaseTraffics.itemno, COUNT(*) AS �D���Ҳ����D����ĵ� into #t13
FROM              CaseTraffics INNER JOIN
                            CaseTrafficPoprocs ON CaseTraffics.Id = CaseTrafficPoprocs.CaseId
							where CaseTrafficPoprocs.EndDateTime is not null and CaseTrafficPoprocs.ViolationsRejectclassId=11 and CaseTrafficPoprocs.PoprocsType=1  and CaseTraffics.InitDate between '2019/1/1' and '2019/3/1'
GROUP BY   CaseTraffics.itemno

SELECT          CaseTraffics.itemno, COUNT(*) AS ��L into #t14
FROM              CaseTraffics INNER JOIN
                            CaseTrafficPoprocs ON CaseTraffics.Id = CaseTrafficPoprocs.CaseId
							where CaseTrafficPoprocs.EndDateTime is not null and CaseTrafficPoprocs.ViolationsRejectclassId=13 and CaseTrafficPoprocs.PoprocsType=1  and CaseTraffics.InitDate between '2019/1/1' and '2019/3/1'
GROUP BY   CaseTraffics.itemno

SELECT          CaseTraffics.itemno, COUNT(*) AS �D�~�ި精��L���� into #t15
FROM              CaseTraffics INNER JOIN
                            CaseTrafficPoprocs ON CaseTraffics.Id = CaseTrafficPoprocs.CaseId
							where CaseTrafficPoprocs.EndDateTime is not null and CaseTrafficPoprocs.ViolationsRejectclassId=14 and CaseTrafficPoprocs.PoprocsType=1  and CaseTraffics.InitDate between '2019/1/1' and '2019/3/1'
GROUP BY   CaseTraffics.itemno

SELECT          CaseTraffics.itemno, COUNT(*) AS �m�W�P�����Ҹ����ŵL�k�d�����|�H���� into #t16
FROM              CaseTraffics INNER JOIN
                            CaseTrafficPoprocs ON CaseTraffics.Id = CaseTrafficPoprocs.CaseId
							where CaseTrafficPoprocs.EndDateTime is not null and CaseTrafficPoprocs.ViolationsRejectclassId=15 and CaseTrafficPoprocs.PoprocsType=1  and CaseTraffics.InitDate between '2019/1/1' and '2019/3/1'
GROUP BY   CaseTraffics.itemno

SELECT        ROW_NUMBER() OVER(order by #t1.itemno desc) AS RowNumber, #t1.itemno,#t1.���z���,  ISNULL(#t2.�|�o���,0) as �|�o���, ISNULL(#t3.�����|�o��ƦX�p,0) as �����|�o��ƦX�p, ISNULL(#t4.�O7�餧���|,0) as �O7�餧���|, ISNULL(#t5.�ɶ�����,0) as �ɶ�����
				,ISNULL( #t6.�a�I����,0) as �a�I����, 
                            ISNULL(#t7.�Ҿڸ����ܦ欰�A�˥��ŹH�W�c���n��,0) as �Ҿڸ����ܦ欰�A�˥��ŹH�W�c���n��, ISNULL(#t8.���˪��Ҿڸ�Ʃ��Ҿڤ���,0) as ���˪��Ҿڸ�Ʃ��Ҿڤ���, ISNULL(#t9.�P�@�ץ󭫽����|,0) as �P�@�ץ󭫽����|, 
                            ISNULL(#t10.���L�H�W�ŦX�k�O�W�w�n��,0) as ���L�H�W�ŦX�k�O�W�w�n��, ISNULL(#t11.���y���P�D���u�{�]�I��ĳ�Ψ�L,0) as ���y���P�D���u�{�]�I��ĳ�Ψ�L, ISNULL(#t12.�ɮ����Y�P�v���ҽk�L�k����,0) as �ɮ����Y�P�v���ҽk�L�k����, 
                            ISNULL(#t13.�D���Ҳ����D����ĵ�,0) as �D���Ҳ����D����ĵ�, ISNULL(#t15.�D�~�ި精��L����,0) as �D�~�ި精��L����, 
                           ISNULL( #t16.�m�W�P�����Ҹ����ŵL�k�d�����|�H����,0) as �m�W�P�����Ҹ����ŵL�k�d�����|�H����, ISNULL(#t14.��L,0) as ��L
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
							
