WITH MyNews AS
(
SELECT    WebCategoryId,count(*) as total
FROM     News 
WHERE   (News.OwnWebSiteId = 1) and (News.StartDate >= '2019/1/1') AND (News.StartDate <= '2019/3/1')
group by WebCategoryId
)

SELECT   '局本部' as Subject,WebNewsCatalogs.Subject as CatalogName,isnull(MyNews.total,0) as Total into #union1
FROM     MyNews RIGHT OUTER JOIN
              WebNewsCatalogs ON MyNews.WebCategoryId = WebNewsCatalogs.Id
;