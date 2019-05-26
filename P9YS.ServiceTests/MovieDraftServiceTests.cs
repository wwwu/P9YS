using Moq;
using P9YS.Services.MovieArea;
using P9YS.Services.MovieDraft;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace P9YS.ServiceTests
{
    public class MovieDraftServiceTests:TestBase
    {
        [Fact]
        public async Task GetMovieDraftDetail_Test()
        {
            //Arrange
            var movieDraft = new EntityFramework.Models.MovieDraft
            {
                Id = 1,
                AddTime = DateTime.Now,
                DoubanHtml = @"<!DOCTYPE html>
<html lang=""zh-cmn-Hans"" class=""ua-windows "">
<head>
<meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"">
<meta name=""renderer"" content=""webkit"">
<meta name=""referrer"" content=""always"">
<meta name=""google-site-verification"" content=""ok0wCgT20tBBgo9_zat2iAcimtN4Ftf5ccsh092Xeyw"" />
<title>
亿男 (豆瓣)
</title>
<meta name=""baidu-site-verification"" content=""cZdR4xxR7RxmM4zE"" />
<meta http-equiv=""Pragma"" content=""no-cache"">
<meta http-equiv=""Expires"" content=""Sun, 6 Mar 2005 01:00:00 GMT"">
<meta name=""keywords"" content=""亿男,億男,亿男影评,剧情介绍,电影图片,预告片,影讯,在线购票,论坛"">
<meta name=""description"" content=""亿男电影简介和剧情介绍,亿男影评、图片、预告片、影讯、论坛、在线购票"">
<meta name=""mobile-agent"" content=""format=html5; url=http://m.douban.com/movie/subject/27604258/""/>
</head>
<body>
<div id=""db-global-nav"" class=""global-nav"">
<div class=""bd"">
<div class=""top-nav-info"">
<a href=""https://accounts.douban.com/passport/login?source=movie"" class=""nav-login"" rel=""nofollow"">登录/注册</a>
</div>
<div class=""top-nav-doubanapp"">
<a href=""https://www.douban.com/doubanapp/app?channel=top-nav"" class=""lnk-doubanapp"">下载豆瓣客户端</a>
<div id=""doubanapp-tip"">
<a href=""https://www.douban.com/doubanapp/app?channel=qipao"" class=""tip-link"">豆瓣 <span class=""version"">6.0</span> 全新发布</a>
<a href=""javascript: void 0;"" class=""tip-close"">×</a>
</div>
<div id=""top-nav-appintro"" class=""more-items"">
<p class=""appintro-title"">豆瓣</p>
<p class=""qrcode"">扫码直接下载</p>
<div class=""download"">
<a href=""https://www.douban.com/doubanapp/redirect?channel=top-nav&direct_dl=1&download=iOS"">iPhone</a>
<span>·</span>
<a href=""https://www.douban.com/doubanapp/redirect?channel=top-nav&direct_dl=1&download=Android"" class=""download-android"">Android</a>
</div>
</div>
</div>
<div class=""global-nav-items"">
<ul>
<li class="""">
<a href=""https://www.douban.com"" target=""_blank"" data-moreurl-dict=""{""from"":""top-nav-click-main"",""uid"":""0""}"">豆瓣</a>
</li>
<li class="""">
<a href=""https://book.douban.com"" target=""_blank"" data-moreurl-dict=""{""from"":""top-nav-click-book"",""uid"":""0""}"">读书</a>
</li>
<li class=""on"">
<a href=""https://movie.douban.com""  data-moreurl-dict=""{""from"":""top-nav-click-movie"",""uid"":""0""}"">电影</a>
</li>
<li class="""">
<a href=""https://music.douban.com"" target=""_blank"" data-moreurl-dict=""{""from"":""top-nav-click-music"",""uid"":""0""}"">音乐</a>
</li>
<li class="""">
<a href=""https://www.douban.com/location"" target=""_blank"" data-moreurl-dict=""{""from"":""top-nav-click-location"",""uid"":""0""}"">同城</a>
</li>
<li class="""">
<a href=""https://www.douban.com/group"" target=""_blank"" data-moreurl-dict=""{""from"":""top-nav-click-group"",""uid"":""0""}"">小组</a>
</li>
<li class="""">
<a href=""https://read.douban.com/?dcs=top-nav&dcm=douban"" target=""_blank"" data-moreurl-dict=""{""from"":""top-nav-click-read"",""uid"":""0""}"">阅读</a>
</li>
<li class="""">
<a href=""https://douban.fm/?from_=shire_top_nav"" target=""_blank"" data-moreurl-dict=""{""from"":""top-nav-click-fm"",""uid"":""0""}"">FM</a>
</li>
<li class="""">
<a href=""https://time.douban.com/?dt_time_source=douban-web_top_nav"" target=""_blank"" data-moreurl-dict=""{""from"":""top-nav-click-time"",""uid"":""0""}"">时间</a>
</li>
<li class="""">
<a href=""https://market.douban.com/?utm_campaign=douban_top_nav&utm_source=douban&utm_medium=pc_web"" target=""_blank"" data-moreurl-dict=""{""from"":""top-nav-click-market"",""uid"":""0""}"">豆品</a>
</li>
<li>
<a href=""#more"" class=""bn-more""><span>更多</span></a>
<div class=""more-items"">
<table cellpadding=""0"" cellspacing=""0"">
<tbody>
<tr>
<td>
<a href=""https://ypy.douban.com"" target=""_blank"" data-moreurl-dict=""{""from"":""top-nav-click-ypy"",""uid"":""0""}"">豆瓣摄影</a>
</td>
</tr>
</tbody>
</table>
</div>
</li>
</ul>
</div>
</div>
</div>
<div id=""db-nav-movie"" class=""nav"">
<div class=""nav-wrap"">
<div class=""nav-primary"">
<div class=""nav-logo"">
<a href=""https://movie.douban.com"">豆瓣电影</a>
</div>
<div class=""nav-search"">
<form action=""https://movie.douban.com/subject_search"" method=""get"">
<fieldset>
<legend>搜索：</legend>
<label for=""inp-query"">
</label>
<div class=""inp""><input id=""inp-query"" name=""search_text"" size=""22"" maxlength=""60"" placeholder=""搜索电影、电视剧、综艺、影人"" value=""""></div>
<div class=""inp-btn""><input type=""submit"" value=""搜索""></div>
<input type=""hidden"" name=""cat"" value=""1002"" />
</fieldset>
</form>
</div>
</div>
</div>
<div class=""nav-secondary"">
<div class=""nav-items"">
<ul>
<li    ><a href=""https://movie.douban.com/cinema/nowplaying/""
>影讯&购票</a>
</li>
<li    ><a href=""https://movie.douban.com/explore""
>选电影</a>
</li>
<li    ><a href=""https://movie.douban.com/tv/""
>电视剧</a>
</li>
<li    ><a href=""https://movie.douban.com/chart""
>排行榜</a>
</li>
<li    ><a href=""https://movie.douban.com/tag/""
>分类</a>
</li>
<li    ><a href=""https://movie.douban.com/review/best/""
>影评</a>
</li>
<li    ><a href=""https://movie.douban.com/annual/2018?source=navigation""
>2018年度榜单</a>
</li>
<li    ><a href=""https://www.douban.com/standbyme/2018?source=navigation""
>2018书影音报告</a>
</li>
</ul>
</div>
<a href=""https://movie.douban.com/annual/2018?source=movie_navigation"" class=""movieannual2018""></a>
</div>
</div>
<div id=""wrapper"">
<div id=""content"">
<div id=""dale_movie_subject_top_icon""></div>
<h1>
<span property=""v:itemreviewed"">亿男 億男</span>
<span class=""year"">(2018)</span>
</h1>
<div class=""grid-16-8 clearfix"">
<div class=""article"">
<div class=""indent clearfix"">
<div class=""subjectwrap clearfix"">
<div class=""subject clearfix"">
<div id=""mainpic"" class="""">
<a class=""nbgnbg"" href=""https://movie.douban.com/subject/27604258/photos?type=R"" title=""点击看更多海报"">
<img src=""https://img1.doubanio.com/view/photo/s_ratio_poster/public/p2529013378.jpg"" title=""点击看更多海报"" alt=""億男"" rel=""v:image"" />
</a>
</div>
<div id=""info"">
<span ><span class='pl'>导演</span>: <span class='attrs'><a href=""/celebrity/1318593/"" rel=""v:directedBy"">大友启史</a></span></span><br/>
<span ><span class='pl'>编剧</span>: <span class='attrs'><a href=""/celebrity/1345696/"">川村元气</a> / <a href=""/subject_search?search_text=%E6%B8%A1%E9%83%A8%E8%BE%B0%E5%9F%8E"">渡部辰城</a></span></span><br/>
<span class=""actor""><span class='pl'>主演</span>: <span class='attrs'><a href=""/celebrity/1227580/"" rel=""v:starring"">佐藤健</a> / <a href=""/celebrity/1275907/"" rel=""v:starring"">高桥一生</a> / <a href=""/celebrity/1327137/"" rel=""v:starring"">黑木华</a> / <a href=""/celebrity/1004857/"" rel=""v:starring"">泽尻英龙华</a> / <a href=""/celebrity/1349142/"" rel=""v:starring"">池田依来沙</a> / <a href=""/celebrity/1045190/"" rel=""v:starring"">藤原龙也</a> / <a href=""/celebrity/1012650/"" rel=""v:starring"">北村一辉</a></span></span><br/>
<span class=""pl"">类型:</span> <span property=""v:genre"">剧情</span><br/>
<span class=""pl"">官方网站:</span> <a href=""http://okuotoko-movie.jp"" rel=""nofollow"" target=""_blank"">okuotoko-movie.jp</a><br/>
<span class=""pl"">制片国家/地区:</span> 日本<br/>
<span class=""pl"">语言:</span> 日语<br/>
<span class=""pl"">上映日期:</span> <span property=""v:initialReleaseDate"" content=""2018-10-19(日本)"">2018-10-19(日本)</span><br/>
<span class=""pl"">IMDb链接:</span> <a href=""http://www.imdb.com/title/tt7754284"" target=""_blank"" rel=""nofollow"">tt7754284</a><br>
</div>
</div>
<div id=""interest_sectl"">
<div class=""rating_wrap clearbox"" rel=""v:rating"">
<div class=""clearfix"">
<div class=""rating_logo ll"">豆瓣评分</div>
<div class=""output-btn-wrap rr"" style=""display:none"">
<img src=""https://img3.doubanio.com/f/movie/692e86756648f29457847c5cc5e161d6f6b8aaac/pics/movie/reference.png"" />
<a class=""download-output-image"" href=""#"">引用</a>
</div>
</div>
<div class=""rating_self clearfix"" typeof=""v:Rating"">
<strong class=""ll rating_num"" property=""v:average"">6.3</strong>
<span property=""v:best"" content=""10.0""></span>
<div class=""rating_right "">
<div class=""ll bigstar bigstar30""></div>
<div class=""rating_sum"">
<a href=""collections"" class=""rating_people""><span property=""v:votes"">941</span>人评价</a>
</div>
</div>
</div>
<div class=""ratings-on-weight"">
<div class=""item"">
<span class=""stars5 starstop"" title=""力荐"">
5星
</span>
<div class=""power"" style=""width:8px""></div>
<span class=""rating_per"">7.4%</span>
<br />
</div>
<div class=""item"">
<span class=""stars4 starstop"" title=""推荐"">
4星
</span>
<div class=""power"" style=""width:24px""></div>
<span class=""rating_per"">20.7%</span>
<br />
</div>
<div class=""item"">
<span class=""stars3 starstop"" title=""还行"">
3星
</span>
<div class=""power"" style=""width:64px""></div>
<span class=""rating_per"">53.1%</span>
<br />
</div>
<div class=""item"">
<span class=""stars2 starstop"" title=""较差"">
2星
</span>
<div class=""power"" style=""width:18px""></div>
<span class=""rating_per"">15.5%</span>
<br />
</div>
<div class=""item"">
<span class=""stars1 starstop"" title=""很差"">
1星
</span>
<div class=""power"" style=""width:4px""></div>
<span class=""rating_per"">3.4%</span>
<br />
</div>
</div>
</div>
</div>
</div>
<div id=""interest_sect_level"" class=""clearfix"">
<a href=""https://www.douban.com/reason=collectwish&ck="" rel=""nofollow"" class=""j a_show_login colbutt ll"" name=""pbtn-27604258-wish"">
<span>想看</span>
</a>
<a href=""https://www.douban.com/reason=collectcollect&ck="" rel=""nofollow"" class=""j a_show_login colbutt ll"" name=""pbtn-27604258-collect"">
<span>看过</span>
</a>
<div class=""ll j a_stars"">
评价:
<span id=""rating""> <span id=""stars"" data-solid=""https://img3.doubanio.com/f/shire/5a2327c04c0c231bced131ddf3f4467eb80c1c86/pics/rating_icons/star_onmouseover.png"" data-hollow=""https://img3.doubanio.com/f/shire/2520c01967207a1735171056ec588c8c1257e5f8/pics/rating_icons/star_hollow_hover.png"" data-solid-2x=""https://img3.doubanio.com/f/shire/7258904022439076d57303c3b06ad195bf1dc41a/pics/rating_icons/star_onmouseover@2x.png"" data-hollow-2x=""https://img3.doubanio.com/f/shire/95cc2fa733221bb8edd28ad56a7145a5ad33383e/pics/rating_icons/star_hollow_hover@2x.png"">
<a href=""https://www.douban.com/register?reason=rate"" class=""j a_show_login"" name=""pbtn-27604258-1"">
<img src=""https://img3.doubanio.com/f/shire/2520c01967207a1735171056ec588c8c1257e5f8/pics/rating_icons/star_hollow_hover.png"" id=""star1"" width=""16"" height=""16""/></a>
<a href=""https://www.douban.com/register?reason=rate"" class=""j a_show_login"" name=""pbtn-27604258-2"">
<img src=""https://img3.doubanio.com/f/shire/2520c01967207a1735171056ec588c8c1257e5f8/pics/rating_icons/star_hollow_hover.png"" id=""star2"" width=""16"" height=""16""/></a>
<a href=""https://www.douban.com/register?reason=rate"" class=""j a_show_login"" name=""pbtn-27604258-3"">
<img src=""https://img3.doubanio.com/f/shire/2520c01967207a1735171056ec588c8c1257e5f8/pics/rating_icons/star_hollow_hover.png"" id=""star3"" width=""16"" height=""16""/></a>
<a href=""https://www.douban.com/register?reason=rate"" class=""j a_show_login"" name=""pbtn-27604258-4"">
<img src=""https://img3.doubanio.com/f/shire/2520c01967207a1735171056ec588c8c1257e5f8/pics/rating_icons/star_hollow_hover.png"" id=""star4"" width=""16"" height=""16""/></a>
<a href=""https://www.douban.com/register?reason=rate"" class=""j a_show_login"" name=""pbtn-27604258-5"">
<img src=""https://img3.doubanio.com/f/shire/2520c01967207a1735171056ec588c8c1257e5f8/pics/rating_icons/star_hollow_hover.png"" id=""star5"" width=""16"" height=""16""/></a>
</span><span id=""rateword"" class=""pl""></span>
<input id=""n_rating"" type=""hidden"" value=""""  />
</span>
</div>
</div>
<div class=""gtleft"">
<ul class=""ul_subject_menu bicelink color_gray pt6 clearfix"">
<li>
<img src=""https://img3.doubanio.com/f/shire/cc03d0fcf32b7ce3af7b160a0b85e5e66b47cc42/pics/short-comment.gif"" /> 
<a onclick=""moreurl(this, {from:'mv_sbj_wr_cmnt_login'})"" class=""j a_show_login"" href=""https://www.douban.com/register?reason=review"" rel=""nofollow"">写短评</a>
</li>
<li>
<img src=""https://img3.doubanio.com/f/shire/5bbf02b7b5ec12b23e214a580b6f9e481108488c/pics/add-review.gif"" /> 
<a onclick=""moreurl(this, {from:'mv_sbj_wr_rv_login'})"" class=""j a_show_login"" href=""https://www.douban.com/register?reason=review"" rel=""nofollow"">写影评</a>
</li>
<li>
<img src=""https://img3.doubanio.com/f/shire/61cc48ba7c40e0272d46bb93fe0dc514f3b71ec5/pics/add-doulist.gif"" /> 
<a class="""" href=""/subject/27604258/questions/ask?from=subject_top"">提问题</a>
</li>
<li>
</li>
<li>
<span class=""rec"" id=""电影-27604258"">
<a href= ""#""
data-type=""电影""
data-url=""https://movie.douban.com/subject/27604258/""
data-desc=""电影《亿男 億男》 (来自豆瓣) ""
data-title=""电影《亿男 億男》 (来自豆瓣) ""
data-pic=""https://img1.doubanio.com/view/photo/s_ratio_poster/public/p2529013378.jpeg""
class=""bn-sharing "">
分享到
</a>   
</span>
</li>
</ul>
</div>
<div class=""rec-sec"">
<span class=""rec"">
<a href=""/accounts/register?reason=recommend""  class=""j a_show_login lnk-sharing"" share-id=""27604258"" data-mode=""plain"" data-name=""亿男 億男‎ (2018)"" data-type=""movie"" data-desc=""导演 大友启史 主演 佐藤健 / 高桥一生 / 日本 / 6.3分(941评价)"" data-href=""https://movie.douban.com/subject/27604258/"" data-image=""https://img1.doubanio.com/view/photo/s_ratio_poster/public/p2529013378.jpg"" data-properties=""{}"" data-redir="""" data-text="""" data-apikey="""" data-curl="""" data-count=""10"" data-object_kind=""1002"" data-object_id=""27604258"" data-target_type=""rec"" data-target_action=""0"" data-action_props=""{""subject_url"":""https:\/\/movie.douban.com\/subject\/27604258\/"",""subject_title"":""亿男 億男‎ (2018)""}"">推荐</a>
</span>
</div>
</div>
<div id=""collect_form_27604258""></div>
<div class=""related-info"" style=""margin-bottom:-10px;"">
<a name=""intro""></a>
<h2>
<i class="""">亿男的剧情简介</i>
· · · · · ·
</h2>
<div class=""indent"" id=""link-report"">
<span property=""v:summary"" class="""">
平凡的圖書館員一男（佐藤健）被弟弟拖累扛下巨額債務，本來愁雲慘澹的日子突然翻盤，一男幸運中了彩券，一夕暴富。然而他卻在網上搜尋發現其他中獎者的悲慘遭遇後，擔心自己也難逃厄運，一時不知該如何處置這筆鉅款。為此一男拜訪了自己多年不見的好友——大富翁九十九（高橋一生），希望能請教九十九使用金錢的方法，以及金錢和幸福間的答案。九十九不吝與一男分享金錢觀，並令一男取出了全部獎金——三億元，希望一男能親身體會身家三億圓的人生，進而知道有關金錢所有的一切，誰料第二天九十九與三億元一起人間蒸發……為了追回巨款，一男循線拜訪了九十九曾經的合伙伙伴。這位擁有億萬身家卻不名一文的「億男」自此踏上了一段驚險、奇妙、不可思議的冒險旅程……（引用自 繁體中文版 博客來平台介紹）
</span>
</div>
</div>
<div id=""celebrities"" class=""celebrities related-celebrities"">
<h2>
<i class="""">亿男的演职员</i>
· · · · · ·
<span class=""pl"">
(
<a href=""/subject/27604258/celebrities"">全部 9</a>
)
</span>
</h2>
<ul class=""celebrities-list from-subject __oneline"">
<li class=""celebrity"">
<a href=""https://movie.douban.com/celebrity/1318593/"" title=""大友启史 Keishi Ohtomo"" class="""">
<div class=""avatar"" style=""background-image: url(https://img3.doubanio.com/view/celebrity/s_ratio_celebrity/public/p1365933969.11.jpg)"">
</div>
</a>
<div class=""info"">
<span class=""name""><a href=""https://movie.douban.com/celebrity/1318593/"" title=""大友启史 Keishi Ohtomo"" class=""name"">大友启史</a></span>
<span class=""role"" title=""导演"">导演</span>
</div>
</li>
<li class=""celebrity"">
<a href=""https://movie.douban.com/celebrity/1227580/"" title=""佐藤健 Takeru Satô"" class="""">
<div class=""avatar"" style=""background-image: url(https://img3.doubanio.com/view/celebrity/s_ratio_celebrity/public/p1419865642.6.jpg)"">
</div>
</a>
<div class=""info"">
<span class=""name""><a href=""https://movie.douban.com/celebrity/1227580/"" title=""佐藤健 Takeru Satô"" class=""name"">佐藤健</a></span>
<span class=""role"" title=""演员"">演员</span>
</div>
</li>
<li class=""celebrity"">
<a href=""https://movie.douban.com/celebrity/1275907/"" title=""高桥一生 Issei Takahashi"" class="""">
<div class=""avatar"" style=""background-image: url(https://img1.doubanio.com/view/celebrity/s_ratio_celebrity/public/p12127.jpg)"">
</div>
</a>
<div class=""info"">
<span class=""name""><a href=""https://movie.douban.com/celebrity/1275907/"" title=""高桥一生 Issei Takahashi"" class=""name"">高桥一生</a></span>
<span class=""role"" title=""演员"">演员</span>
</div>
</li>
<li class=""celebrity"">
<a href=""https://movie.douban.com/celebrity/1327137/"" title=""黑木华 Haru Kuroki"" class="""">
<div class=""avatar"" style=""background-image: url(https://img1.doubanio.com/view/celebrity/s_ratio_celebrity/public/p1367243985.68.jpg)"">
</div>
</a>
<div class=""info"">
<span class=""name""><a href=""https://movie.douban.com/celebrity/1327137/"" title=""黑木华 Haru Kuroki"" class=""name"">黑木华</a></span>
<span class=""role"" title=""演员"">演员</span>
</div>
</li>
<li class=""celebrity"">
<a href=""https://movie.douban.com/celebrity/1004857/"" title=""泽尻英龙华 Erika Sawajiri"" class="""">
<div class=""avatar"" style=""background-image: url(https://img3.doubanio.com/view/celebrity/s_ratio_celebrity/public/p10684.jpg)"">
</div>
</a>
<div class=""info"">
<span class=""name""><a href=""https://movie.douban.com/celebrity/1004857/"" title=""泽尻英龙华 Erika Sawajiri"" class=""name"">泽尻英龙华</a></span>
<span class=""role"" title=""演员"">演员</span>
</div>
</li>
<li class=""celebrity"">
<a href=""https://movie.douban.com/celebrity/1349142/"" title=""池田依来沙 Elaiza Ikeda"" class="""">
<div class=""avatar"" style=""background-image: url(https://img3.doubanio.com/view/celebrity/s_ratio_celebrity/public/p1434442623.64.jpg)"">
</div>
</a>
<div class=""info"">
<span class=""name""><a href=""https://movie.douban.com/celebrity/1349142/"" title=""池田依来沙 Elaiza Ikeda"" class=""name"">池田依来沙</a></span>
<span class=""role"" title=""演员"">演员</span>
</div>
</li>
</ul>
</div>
<div id=""author_subject"" class=""author-wrapper"">
<div class=""loading""></div>
</div>
<div id=""related-pic"" class=""related-pic"">
<h2>
<i class="""">亿男的视频和图片</i>
· · · · · ·
<span class=""pl"">
(
<a href=""https://movie.douban.com/subject/27604258/trailer#trailer"">预告片5</a> | <a href=""/video/create?subject_id=27604258"">添加视频评论</a> | <a href=""https://movie.douban.com/subject/27604258/all_photos"">图片200</a> · <a href=""https://movie.douban.com/subject/27604258/mupload"">添加</a>
)
</span>
</h2>
<ul class=""related-pic-bd  "">
<li class=""label-trailer"">
<a class=""related-pic-video"" href=""https://movie.douban.com/trailer/241988/#content"" title=""预告片"" style=""background-image:url(https://img3.doubanio.com/img/trailer/medium/2545427991.jpg?)"">
</a>
</li>
<li>
<a href=""https://movie.douban.com/photos/photo/2530497770/""><img src=""https://img3.doubanio.com/view/photo/sqxs/public/p2530497770.jpg"" alt=""图片"" /></a>
</li>
<li>
<a href=""https://movie.douban.com/photos/photo/2530497760/""><img src=""https://img3.doubanio.com/view/photo/sqxs/public/p2530497760.jpg"" alt=""图片"" /></a>
</li>
<li>
<a href=""https://movie.douban.com/photos/photo/2530497958/""><img src=""https://img1.doubanio.com/view/photo/sqxs/public/p2530497958.jpg"" alt=""图片"" /></a>
</li>
<li>
<a href=""https://movie.douban.com/photos/photo/2530497955/""><img src=""https://img3.doubanio.com/view/photo/sqxs/public/p2530497955.jpg"" alt=""图片"" /></a>
</li>
</ul>
</div>
<div id=""recommendations"" class="""">
<h2>
<i class="""">喜欢这部电影的人也喜欢</i>
· · · · · ·
</h2>
<div class=""recommendations-bd"">
<dl class="""">
<dt>
<a href=""https://movie.douban.com/subject/27124421/?from=subject-page"" >
<img src=""https://img3.doubanio.com/view/photo/s_ratio_poster/public/p2528327496.jpg"" alt=""仙踪乐园"" class="""" />
</a>
</dt>
<dd>
<a href=""https://movie.douban.com/subject/27124421/?from=subject-page"" class="""" >仙踪乐园</a>
</dd>
</dl>
<dl class="""">
<dt>
<a href=""https://movie.douban.com/subject/27113986/?from=subject-page"" >
<img src=""https://img3.doubanio.com/view/photo/s_ratio_poster/public/p2526771393.jpg"" alt=""爱哭鬼的奇迹"" class="""" />
</a>
</dt>
<dd>
<a href=""https://movie.douban.com/subject/27113986/?from=subject-page"" class="""" >爱哭鬼的奇迹</a>
</dd>
</dl>
<dl class="""">
<dt>
<a href=""https://movie.douban.com/subject/25842468/?from=subject-page"" >
<img src=""https://img3.doubanio.com/view/photo/s_ratio_poster/public/p2552076055.jpg"" alt=""音乐教师"" class="""" />
</a>
</dt>
<dd>
<a href=""https://movie.douban.com/subject/25842468/?from=subject-page"" class="""" >音乐教师</a>
</dd>
</dl>
<dl class="""">
<dt>
<a href=""https://movie.douban.com/subject/30390982/?from=subject-page"" >
<img src=""https://img1.doubanio.com/view/photo/s_ratio_poster/public/p2541279809.jpg"" alt=""她唯一的选择"" class="""" />
</a>
</dt>
<dd>
<a href=""https://movie.douban.com/subject/30390982/?from=subject-page"" class="""" >她唯一的选择</a>
</dd>
</dl>
<dl class="""">
<dt>
<a href=""https://movie.douban.com/subject/33408831/?from=subject-page"" >
<img src=""https://img3.doubanio.com/view/photo/s_ratio_poster/public/p2553459216.jpg"" alt=""跨州轶事录"" class="""" />
</a>
</dt>
<dd>
<a href=""https://movie.douban.com/subject/33408831/?from=subject-page"" class="""" >跨州轶事录</a>
</dd>
</dl>
<dl class="""">
<dt>
<a href=""https://movie.douban.com/subject/27035675/?from=subject-page"" >
<img src=""https://img1.doubanio.com/view/photo/s_ratio_poster/public/p2520537829.jpg"" alt=""愿上帝降临"" class="""" />
</a>
</dt>
<dd>
<a href=""https://movie.douban.com/subject/27035675/?from=subject-page"" class="""" >愿上帝降临</a>
</dd>
</dl>
<dl class="""">
<dt>
<a href=""https://movie.douban.com/subject/30400620/?from=subject-page"" >
<img src=""https://img3.doubanio.com/view/photo/s_ratio_poster/public/p2543631370.jpg"" alt=""三天两夜"" class="""" />
</a>
</dt>
<dd>
<a href=""https://movie.douban.com/subject/30400620/?from=subject-page"" class="""" >三天两夜</a>
</dd>
</dl>
<dl class="""">
<dt>
<a href=""https://movie.douban.com/subject/27155458/?from=subject-page"" >
<img src=""https://img3.doubanio.com/view/photo/s_ratio_poster/public/p2525677723.jpg"" alt=""爸爸是坏人冠军"" class="""" />
</a>
</dt>
<dd>
<a href=""https://movie.douban.com/subject/27155458/?from=subject-page"" class="""" >爸爸是坏人冠军</a>
</dd>
</dl>
<dl class="""">
<dt>
<a href=""https://movie.douban.com/subject/26944593/?from=subject-page"" >
<img src=""https://img3.doubanio.com/view/photo/s_ratio_poster/public/p2541676394.jpg"" alt=""泰德：爱的缘故"" class="""" />
</a>
</dt>
<dd>
<a href=""https://movie.douban.com/subject/26944593/?from=subject-page"" class="""" >泰德：爱的缘故</a>
</dd>
</dl>
<dl class="""">
<dt>
<a href=""https://movie.douban.com/subject/30210095/?from=subject-page"" >
<img src=""https://img3.doubanio.com/view/photo/s_ratio_poster/public/p2550695426.jpg"" alt=""搬家的大名"" class="""" />
</a>
</dt>
<dd>
<a href=""https://movie.douban.com/subject/30210095/?from=subject-page"" class="""" >搬家的大名</a>
</dd>
</dl>
</div>
</div>
<div id=""comments-section"">
<div class=""mod-hd"">
<a class=""comment_btn j a_show_login"" href=""https://www.douban.com/register?reason=review"" rel=""nofollow"">
<span>我要写短评</span>
</a>
<h2>
<i class="""">亿男的短评</i>
· · · · · ·
<span class=""pl"">
(
<a href=""https://movie.douban.com/subject/27604258/comments?status=P"">全部 347 条</a>
)
</span>
</h2>
</div>
<div class=""mod-bd"">
<div class=""tab-hd"">
<a id=""hot-comments-tab"" href=""comments"" data-id=""hot"" class=""on"">热门</a> / 
<a id=""new-comments-tab"" href=""comments?sort=time"" data-id=""new"">最新</a> / 
<a id=""following-comments-tab"" href=""follows_comments"" data-id=""following""  class=""j a_show_login"">好友</a>
</div>
<div class=""tab-bd"">
<div id=""hot-comments"" class=""tab"">
<div class=""comment-item"" data-cid=""1500366671"">
<div class=""comment"">
<h3>
<span class=""comment-vote"">
<span class=""votes"">8</span>
<input value=""1500366671"" type=""hidden""/>
<a href=""javascript:;"" class=""j a_show_login"" onclick="""">有用</a>
</span>
<span class=""comment-info"">
<a href=""https://www.douban.com/people/cryingcatjojo/"" class="""">丁小猫</a>
<span>看过</span>
<span class=""allstar40 rating"" title=""推荐""></span>
<span class=""comment-time "" title=""2018-10-21 17:19:47"">
2018-10-21
</span>
</span>
</h3>
<p class="""">
<span class=""short"">三星给我高桥老公，一星给饼哥的假肚子。</span>
</p>
</div>
</div>
<div class=""comment-item"" data-cid=""1450037287"">
<div class=""comment"">
<h3>
<span class=""comment-vote"">
<span class=""votes"">4</span>
<input value=""1450037287"" type=""hidden""/>
<a href=""javascript:;"" class=""j a_show_login"" onclick="""">有用</a>
</span>
<span class=""comment-info"">
<a href=""https://www.douban.com/people/22406636/"" class="""">盲人薛</a>
<span>看过</span>
<span class=""allstar30 rating"" title=""还行""></span>
<span class=""comment-time "" title=""2018-10-24 22:55:33"">
2018-10-24
</span>
</span>
</h3>
<p class="""">
<span class=""short"">高桥一生好萌，好喜欢大学时期乖巧的发型，还有跪在沙漠中讲落语的段落，美好到不想眨眼。故事挺感人的，但是这个导演真不是我的菜……</span>
</p>
</div>
</div>
<div class=""comment-item"" data-cid=""1788928920"">
<div class=""comment"">
<h3>
<span class=""comment-vote"">
<span class=""votes"">1</span>
<input value=""1788928920"" type=""hidden""/>
<a href=""javascript:;"" class=""j a_show_login"" onclick="""">有用</a>
</span>
<span class=""comment-info"">
<a href=""https://www.douban.com/people/glim/"" class="""">glim</a>
<span>看过</span>
<span class=""allstar10 rating"" title=""很差""></span>
<span class=""comment-time "" title=""2019-05-17 10:09:39"">
2019-05-17
</span>
</span>
</h3>
<p class="""">
<span class=""short"">用全明星阵容堆了一坨屎，摩洛哥那段故作深沉的烘托真是费劲</span>
</p>
</div>
</div>
<div class=""comment-item"" data-cid=""1572756286"">
<div class=""comment"">
<h3>
<span class=""comment-vote"">
<span class=""votes"">1</span>
<input value=""1572756286"" type=""hidden""/>
<a href=""javascript:;"" class=""j a_show_login"" onclick="""">有用</a>
</span>
<span class=""comment-info"">
<a href=""https://www.douban.com/people/willingnotto/"" class="""">Rhodesia</a>
<span>看过</span>
<span class=""allstar30 rating"" title=""还行""></span>
<span class=""comment-time "" title=""2018-12-15 17:41:20"">
2018-12-15
</span>
</span>
</h3>
<p class="""">
<span class=""short"">比猫片略好一点吧，要是拍成大陆版应该很可怕…</span>
</p>
</div>
</div>
<div class=""comment-item"" data-cid=""1499675995"">
<div class=""comment"">
<h3>
<span class=""comment-vote"">
<span class=""votes"">3</span>
<input value=""1499675995"" type=""hidden""/>
<a href=""javascript:;"" class=""j a_show_login"" onclick="""">有用</a>
</span>
<span class=""comment-info"">
<a href=""https://www.douban.com/people/37737818/"" class="""">空っぽ</a>
<span>看过</span>
<span class=""allstar30 rating"" title=""还行""></span>
<span class=""comment-time "" title=""2018-10-20 22:39:02"">
2018-10-20
</span>
</span>
</h3>
<p class="""">
<span class=""short"">去听歌的。本来还挺期待剧情，结果整个节奏温吞无高潮，后半强忍睡意直到主题曲响起才清醒😒并且全剧主旨大概就是卖腐吧</span>
</p>
</div>
</div>
> <a href=""comments?sort=new_score&status=P"" >更多短评347条</a>
</div>
<div id=""new-comments"" class=""tab"">
<div id=""normal"">
</div>
<div class=""fold-hd hide"">
<a class=""qa"" href=""/help/opinion#t2-q0"" target=""_blank"">为什么被折叠？</a>
<a class=""btn-unfold"" href=""#"">有一些短评被折叠了</a>
<div class=""qa-tip"">
评论被折叠，是因为发布这条评论的帐号行为异常。评论仍可以被展开阅读，对发布人的账号不造成其他影响。如果认为有问题，可以<a href=""https://help.douban.com/help/ask?category=movie"">联系</a>豆瓣电影。
</div>
</div>
<div class=""fold-bd"">
</div>
<span id=""total-num""></span>
</div>
<div id=""following-comments"" class=""tab"">
<div class=""comment-item"">
你关注的人还没写过短评
</div>
</div>
</div>
</div>
</div>
<section class=""topics mod"">
<header>
<h2>
亿男的话题 · · · · · ·
<span class=""pl"">( <span class=""gallery_topics"">全部 <span id=""topic-count""></span> 条</span> )</span>
</h2>
</header>
<section class=""subject-topics"">
<div class=""topic-guide"" id=""topic-guide"">
<img class=""ic_question"" src=""//img3.doubanio.com/f/ithildin/b1a3edea3d04805f899e9d77c0bfc0d158df10d5/pics/export/icon_question.png"">
<div class=""tip_content"">
<div class=""tip_title"">什么是话题</div>
<div class=""tip_desc"">
<div>无论是一部作品、一个人，还是一件事，都往往可以衍生出许多不同的话题。将这些话题细分出来，分别进行讨论，会有更多收获。</div>
</div>
</div>
<img class=""ic_guide"" src=""//img3.doubanio.com/f/ithildin/529f46d86bc08f55cd0b1843d0492242ebbd22de/pics/export/icon_guide_arrow.png"">
<img class=""ic_close"" id=""topic-guide-close"" src=""//img3.doubanio.com/f/ithildin/2eb4ad488cb0854644b23f20b6fa312404429589/pics/export/close@3x.png"">
</div>
<div id=""topic-items""></div>
</section>
</section>
<section class=""reviews mod movie-content"">
<header>
<a href=""new_review"" rel=""nofollow"" class=""create-review comment_btn ""
data-isverify=""False""
data-verify-url=""https://www.douban.com/accounts/phone/verify?redir=http://movie.douban.com/subject/27604258/new_review"">
<span>我要写影评</span>
</a>
<h2>
亿男的影评 · · · · · ·
<span class=""pl"">( <a href=""reviews"">全部 11 条</a> )</span>
</h2>
</header>
<div class=""review_filter"">
<a href=""javascript:;;"" class=""cur"" data-sort="""">热门</a href=""javascript:;;""> /
<a href=""javascript:;;"" data-sort=""time"">最新</a href=""javascript:;;""> /
<a href=""javascript:;;"" data-sort=""follow"">好友</a href=""javascript:;;"">
</div>
<div class=""review-list  "">
<div data-cid=""10175000"">
<div class=""main review-item"" id=""10175000"">
<header class=""main-hd"">
<a href=""https://www.douban.com/people/babulliu/"" class=""avator"">
<img width=""24"" height=""24"" src=""https://img3.doubanio.com/icon/u42097517-14.jpg"">
</a>
<a href=""https://www.douban.com/people/babulliu/"" class=""name"">把噗</a>
<span class=""allstar30 main-title-rating"" title=""还行""></span>
<span content=""2019-05-12"" class=""main-meta"">2019-05-12 12:14:30</span>
</header>
<div class=""main-bd"">
<h2><a href=""https://movie.douban.com/review/10175000/"">一夜暴富的故事，没有你想得那么美好</a></h2>
<div id=""review_10175000_short"" class=""review-short"" data-rid=""10175000"">
<div class=""short-content"">
<p class=""spoiler-tip"">这篇影评可能有剧透</p>
《亿男》的故事，简单得不能再简单。图书管理员一男因弟弟拖累扛下巨额债务，却不想中彩票一夜暴富，正当不知如何处置这比巨款之时，多年不见的挚友现身，并带着巨款消失，一男为此踏上寻找金钱和真相的旅程。 这个略带寓言性质的故事原本不必如此讲述。一种更为直白的版本是，...
 (<a href=""javascript:;"" id=""toggle-10175000-copy"" class=""unfold"" title=""展开"">展开</a>)
</div>
</div>
<div id=""review_10175000_full"" class=""hidden"">
<div id=""review_10175000_full_content"" class=""full-content""></div>
</div>
<div class=""action"">
<a href=""javascript:;"" class=""action-btn up"" data-rid=""10175000"" title=""有用"">
<img src=""https://img3.doubanio.com/f/zerkalo/536fd337139250b5fb3cf9e79cb65c6193f8b20b/pics/up.png"" />
<span id=""r-useful_count-10175000"">
</span>
</a>
<a href=""javascript:;"" class=""action-btn down"" data-rid=""10175000"" title=""没用"">
<img src=""https://img3.doubanio.com/f/zerkalo/68849027911140623cf338c9845893c4566db851/pics/down.png"" />
<span id=""r-useless_count-10175000"">
</span>
</a>
<a href=""https://movie.douban.com/review/10175000/#comments"" class=""reply "">0回应</a>
<a href=""javascript:;;"" class=""fold hidden"">收起</a>
</div>
</div>
</div>
</div>
<div data-cid=""10113384"">
<div class=""main review-item"" id=""10113384"">
<header class=""main-hd"">
<a href=""https://www.douban.com/people/61065423/"" class=""avator"">
<img width=""24"" height=""24"" src=""https://img3.doubanio.com/icon/u61065423-95.jpg"">
</a>
<a href=""https://www.douban.com/people/61065423/"" class=""name"">威風堂々</a>
<span class=""allstar20 main-title-rating"" title=""较差""></span>
<span content=""2019-04-14"" class=""main-meta"">2019-04-14 20:23:29</span>
</header>
<div class=""main-bd"">
<h2><a href=""https://movie.douban.com/review/10113384/"">尴尬的强行鸡汤和说教</a></h2>
<div id=""review_10113384_short"" class=""review-short"" data-rid=""10113384"">
<div class=""short-content"">
<p class=""spoiler-tip"">这篇影评可能有剧透</p>
当一个人背负一屁股债天天两份工作严重睡眠不足身体快被掏空并因此不能与妻女住在一起，你跟我说不是有钱就能解决一切？如果钱不能解决他的问题那什么能解决？强行鸡汤和说教吗？ 对不起，对我来说钱就是能解决他一切的燃眉之急，而且也只有钱能解决，这个故事在他中奖之后就能...
 (<a href=""javascript:;"" id=""toggle-10113384-copy"" class=""unfold"" title=""展开"">展开</a>)
</div>
</div>
<div id=""review_10113384_full"" class=""hidden"">
<div id=""review_10113384_full_content"" class=""full-content""></div>
</div>
<div class=""action"">
<a href=""javascript:;"" class=""action-btn up"" data-rid=""10113384"" title=""有用"">
<img src=""https://img3.doubanio.com/f/zerkalo/536fd337139250b5fb3cf9e79cb65c6193f8b20b/pics/up.png"" />
<span id=""r-useful_count-10113384"">
7
</span>
</a>
<a href=""javascript:;"" class=""action-btn down"" data-rid=""10113384"" title=""没用"">
<img src=""https://img3.doubanio.com/f/zerkalo/68849027911140623cf338c9845893c4566db851/pics/down.png"" />
<span id=""r-useless_count-10113384"">
1
</span>
</a>
<a href=""https://movie.douban.com/review/10113384/#comments"" class=""reply "">8回应</a>
<a href=""javascript:;;"" class=""fold hidden"">收起</a>
</div>
</div>
</div>
</div>
<div data-cid=""10188376"">
<div class=""main review-item"" id=""10188376"">
<header class=""main-hd"">
<a href=""https://www.douban.com/people/3050461/"" class=""avator"">
<img width=""24"" height=""24"" src=""https://img3.doubanio.com/icon/u3050461-2.jpg"">
</a>
<a href=""https://www.douban.com/people/3050461/"" class=""name"">树不知</a>
<span class=""allstar40 main-title-rating"" title=""推荐""></span>
<span content=""2019-05-18"" class=""main-meta"">2019-05-18 22:03:53</span>
</header>
<div class=""main-bd"">
<h2><a href=""https://movie.douban.com/review/10188376/"">要为真正重要的东西而活</a></h2>
<div id=""review_10188376_short"" class=""review-short"" data-rid=""10188376"">
<div class=""short-content"">
<p class=""spoiler-tip"">这篇影评可能有剧透</p>
一男和九十九，前者充满幻想，后者充满神秘。于是从故事的开始就让人不断脑补，这片会像《心理游戏》那样，一切其实都是九十九安排的套路。 只是不知道究竟是九十九用一男中彩票得来的3亿元安排的戏，抑或那中彩票和3亿元本来就由百亿富豪九十九为了寻回初心而准备的。姑且抛开...
 (<a href=""javascript:;"" id=""toggle-10188376-copy"" class=""unfold"" title=""展开"">展开</a>)
</div>
</div>
<div id=""review_10188376_full"" class=""hidden"">
<div id=""review_10188376_full_content"" class=""full-content""></div>
</div>
<div class=""action"">
<a href=""javascript:;"" class=""action-btn up"" data-rid=""10188376"" title=""有用"">
<img src=""https://img3.doubanio.com/f/zerkalo/536fd337139250b5fb3cf9e79cb65c6193f8b20b/pics/up.png"" />
<span id=""r-useful_count-10188376"">
</span>
</a>
<a href=""javascript:;"" class=""action-btn down"" data-rid=""10188376"" title=""没用"">
<img src=""https://img3.doubanio.com/f/zerkalo/68849027911140623cf338c9845893c4566db851/pics/down.png"" />
<span id=""r-useless_count-10188376"">
</span>
</a>
<a href=""https://movie.douban.com/review/10188376/#comments"" class=""reply "">0回应</a>
<a href=""javascript:;;"" class=""fold hidden"">收起</a>
</div>
</div>
</div>
</div>
<div data-cid=""10181586"">
<div class=""main review-item"" id=""10181586"">
<header class=""main-hd"">
<a href=""https://www.douban.com/people/194290214/"" class=""avator"">
<img width=""24"" height=""24"" src=""https://img3.doubanio.com/icon/u194290214-1.jpg"">
</a>
<a href=""https://www.douban.com/people/194290214/"" class=""name""></a>
<span class=""allstar30 main-title-rating"" title=""还行""></span>
<span content=""2019-05-15"" class=""main-meta"">2019-05-15 10:07:19</span>
</header>
<div class=""main-bd"">
<h2><a href=""https://movie.douban.com/review/10181586/"">看就完了</a></h2>
<div id=""review_10181586_short"" class=""review-short"" data-rid=""10181586"">
<div class=""short-content"">
平凡的圖書館員一男（佐藤健）被弟弟拖累扛下巨額債務，本來愁雲慘澹的日子突然翻盤，一男幸運中了彩券，一夕暴富。然而他卻在網上搜尋發現其他中獎者的悲慘遭遇後，擔心自己也難逃厄運，一時不知該如何處置這筆鉅款。為此一男拜訪了自己多年不見的好友——大富翁九十九（高橋...
 (<a href=""javascript:;"" id=""toggle-10181586-copy"" class=""unfold"" title=""展开"">展开</a>)
</div>
</div>
<div id=""review_10181586_full"" class=""hidden"">
<div id=""review_10181586_full_content"" class=""full-content""></div>
</div>
<div class=""action"">
<a href=""javascript:;"" class=""action-btn up"" data-rid=""10181586"" title=""有用"">
<img src=""https://img3.doubanio.com/f/zerkalo/536fd337139250b5fb3cf9e79cb65c6193f8b20b/pics/up.png"" />
<span id=""r-useful_count-10181586"">
</span>
</a>
<a href=""javascript:;"" class=""action-btn down"" data-rid=""10181586"" title=""没用"">
<img src=""https://img3.doubanio.com/f/zerkalo/68849027911140623cf338c9845893c4566db851/pics/down.png"" />
<span id=""r-useless_count-10181586"">
</span>
</a>
<a href=""https://movie.douban.com/review/10181586/#comments"" class=""reply "">0回应</a>
<a href=""javascript:;;"" class=""fold hidden"">收起</a>
</div>
</div>
</div>
</div>
<div data-cid=""10181191"">
<div class=""main review-item"" id=""10181191"">
<header class=""main-hd"">
<a href=""https://www.douban.com/people/183303569/"" class=""avator"">
<img width=""24"" height=""24"" src=""https://img3.doubanio.com/icon/u183303569-30.jpg"">
</a>
<a href=""https://www.douban.com/people/183303569/"" class=""name"">光年之外</a>
<span class=""allstar40 main-title-rating"" title=""推荐""></span>
<span content=""2019-05-15"" class=""main-meta"">2019-05-15 00:22:11</span>
</header>
<div class=""main-bd"">
<h2><a href=""https://movie.douban.com/review/10181191/"">这是一部讲钱是什么的电影，一夜暴富，基本没有好下场</a></h2>
<div id=""review_10181191_short"" class=""review-short"" data-rid=""10181191"">
<div class=""short-content"">
昨天在看NBA考古季，然后翻到一个采访巴克利的文章，内容是退役NBA球员破产率是80%，因为这些运动员没有理解钱，在役的时候收入很高，退役后很快没了收入，却依旧保持一样的花钱速度，所及80%的退役球员都破产。我知道破产的NBA明星不少，包括大红大紫的AI答案艾弗森、大虫罗德...
 (<a href=""javascript:;"" id=""toggle-10181191-copy"" class=""unfold"" title=""展开"">展开</a>)
</div>
</div>
<div id=""review_10181191_full"" class=""hidden"">
<div id=""review_10181191_full_content"" class=""full-content""></div>
</div>
<div class=""action"">
<a href=""javascript:;"" class=""action-btn up"" data-rid=""10181191"" title=""有用"">
<img src=""https://img3.doubanio.com/f/zerkalo/536fd337139250b5fb3cf9e79cb65c6193f8b20b/pics/up.png"" />
<span id=""r-useful_count-10181191"">
1
</span>
</a>
<a href=""javascript:;"" class=""action-btn down"" data-rid=""10181191"" title=""没用"">
<img src=""https://img3.doubanio.com/f/zerkalo/68849027911140623cf338c9845893c4566db851/pics/down.png"" />
<span id=""r-useless_count-10181191"">
</span>
</a>
<a href=""https://movie.douban.com/review/10181191/#comments"" class=""reply "">0回应</a>
<a href=""javascript:;;"" class=""fold hidden"">收起</a>
</div>
</div>
</div>
</div>
<div data-cid=""10180310"">
<div class=""main review-item"" id=""10180310"">
<header class=""main-hd"">
<a href=""https://www.douban.com/people/188896273/"" class=""avator"">
<img width=""24"" height=""24"" src=""https://img3.doubanio.com/icon/u188896273-1.jpg"">
</a>
<a href=""https://www.douban.com/people/188896273/"" class=""name"">李元芳66</a>
<span class=""allstar30 main-title-rating"" title=""还行""></span>
<span content=""2019-05-14"" class=""main-meta"">2019-05-14 17:54:45</span>
</header>
<div class=""main-bd"">
<h2><a href=""https://movie.douban.com/review/10180310/"">…</a></h2>
<div id=""review_10180310_short"" class=""review-short"" data-rid=""10180310"">
<div class=""short-content"">
版权归作者所有，任何形式转载请联系作者。 作者：威風堂々（来自豆瓣） 来源：https://www.douban.com/doubanapp/dispatch/review/10113384 妻子也是无与伦比的矫情，芭蕾那段让人极度不适。不是，你希望丈夫能支持女儿唯一的爱好没错，但是能别这么理直气壮吗？起早贪黑努力...
 (<a href=""javascript:;"" id=""toggle-10180310-copy"" class=""unfold"" title=""展开"">展开</a>)
</div>
</div>
<div id=""review_10180310_full"" class=""hidden"">
<div id=""review_10180310_full_content"" class=""full-content""></div>
</div>
<div class=""action"">
<a href=""javascript:;"" class=""action-btn up"" data-rid=""10180310"" title=""有用"">
<img src=""https://img3.doubanio.com/f/zerkalo/536fd337139250b5fb3cf9e79cb65c6193f8b20b/pics/up.png"" />
<span id=""r-useful_count-10180310"">
</span>
</a>
<a href=""javascript:;"" class=""action-btn down"" data-rid=""10180310"" title=""没用"">
<img src=""https://img3.doubanio.com/f/zerkalo/68849027911140623cf338c9845893c4566db851/pics/down.png"" />
<span id=""r-useless_count-10180310"">
</span>
</a>
<a href=""https://movie.douban.com/review/10180310/#comments"" class=""reply "">0回应</a>
<a href=""javascript:;;"" class=""fold hidden"">收起</a>
</div>
</div>
</div>
</div>
<div data-cid=""10179685"">
<div class=""main review-item"" id=""10179685"">
<header class=""main-hd"">
<a href=""https://www.douban.com/people/77299768/"" class=""avator"">
<img width=""24"" height=""24"" src=""https://img3.doubanio.com/icon/u77299768-5.jpg"">
</a>
<a href=""https://www.douban.com/people/77299768/"" class=""name"">柳三行</a>
<span class=""allstar30 main-title-rating"" title=""还行""></span>
<span content=""2019-05-14"" class=""main-meta"">2019-05-14 12:40:38</span>
</header>
<div class=""main-bd"">
<h2><a href=""https://movie.douban.com/review/10179685/"">从我的一次彩票中奖说起</a></h2>
<div id=""review_10179685_short"" class=""review-short"" data-rid=""10179685"">
<div class=""short-content"">
还是学生时的夏天，周末去杭州见朋友，那天头一直很疼，公交车内，汽油味和塑料座椅散发的气味让我感到恶心，就和朋友提前下车了。下车走了一会儿，捡到一百块。我正犹豫是谁掉的，前面一位婴儿肥马尾辫，白T淡蓝背带牛仔裤的姑娘，应该是高中生吧，也可能是初中生，朝我们方向...
 (<a href=""javascript:;"" id=""toggle-10179685-copy"" class=""unfold"" title=""展开"">展开</a>)
</div>
</div>
<div id=""review_10179685_full"" class=""hidden"">
<div id=""review_10179685_full_content"" class=""full-content""></div>
</div>
<div class=""action"">
<a href=""javascript:;"" class=""action-btn up"" data-rid=""10179685"" title=""有用"">
<img src=""https://img3.doubanio.com/f/zerkalo/536fd337139250b5fb3cf9e79cb65c6193f8b20b/pics/up.png"" />
<span id=""r-useful_count-10179685"">
2
</span>
</a>
<a href=""javascript:;"" class=""action-btn down"" data-rid=""10179685"" title=""没用"">
<img src=""https://img3.doubanio.com/f/zerkalo/68849027911140623cf338c9845893c4566db851/pics/down.png"" />
<span id=""r-useless_count-10179685"">
</span>
</a>
<a href=""https://movie.douban.com/review/10179685/#comments"" class=""reply "">0回应</a>
<a href=""javascript:;;"" class=""fold hidden"">收起</a>
</div>
</div>
</div>
</div>
<div data-cid=""10179646"">
<div class=""main review-item"" id=""10179646"">
<header class=""main-hd"">
<a href=""https://www.douban.com/people/171209888/"" class=""avator"">
<img width=""24"" height=""24"" src=""https://img3.doubanio.com/icon/u171209888-1.jpg"">
</a>
<a href=""https://www.douban.com/people/171209888/"" class=""name"">冬冬</a>
<span class=""allstar50 main-title-rating"" title=""力荐""></span>
<span content=""2019-05-14"" class=""main-meta"">2019-05-14 12:12:55</span>
</header>
<div class=""main-bd"">
<h2><a href=""https://movie.douban.com/review/10179646/"">神剧</a></h2>
<div id=""review_10179646_short"" class=""review-short"" data-rid=""10179646"">
<div class=""short-content"">
<p class=""spoiler-tip"">这篇影评可能有剧透</p>
神剧 电影剧本导演配乐都非常优秀 如果写一个剧情简介实在太不尊重电影里所表达的深度及意义 电影里有很多的剧情及细节刻画都能拉出来单独写一篇论文。一枚硬币和一张纸币的价值和价格的区别 。金钱发展成为宗教 它具有腐蚀人心的力量 如同相对论里大质量恒星塌陷终结成黑洞 扭...
 (<a href=""javascript:;"" id=""toggle-10179646-copy"" class=""unfold"" title=""展开"">展开</a>)
</div>
</div>
<div id=""review_10179646_full"" class=""hidden"">
<div id=""review_10179646_full_content"" class=""full-content""></div>
</div>
<div class=""action"">
<a href=""javascript:;"" class=""action-btn up"" data-rid=""10179646"" title=""有用"">
<img src=""https://img3.doubanio.com/f/zerkalo/536fd337139250b5fb3cf9e79cb65c6193f8b20b/pics/up.png"" />
<span id=""r-useful_count-10179646"">
1
</span>
</a>
<a href=""javascript:;"" class=""action-btn down"" data-rid=""10179646"" title=""没用"">
<img src=""https://img3.doubanio.com/f/zerkalo/68849027911140623cf338c9845893c4566db851/pics/down.png"" />
<span id=""r-useless_count-10179646"">
</span>
</a>
<a href=""https://movie.douban.com/review/10179646/#comments"" class=""reply "">0回应</a>
<a href=""javascript:;;"" class=""fold hidden"">收起</a>
</div>
</div>
</div>
</div>
<div data-cid=""10179418"">
<div class=""main review-item"" id=""10179418"">
<header class=""main-hd"">
<a href=""https://www.douban.com/people/190697707/"" class=""avator"">
<img width=""24"" height=""24"" src=""https://img3.doubanio.com/icon/u190697707-2.jpg"">
</a>
<a href=""https://www.douban.com/people/190697707/"" class=""name"">蓝天超市</a>
<span class=""allstar40 main-title-rating"" title=""推荐""></span>
<span content=""2019-05-14"" class=""main-meta"">2019-05-14 10:12:09</span>
</header>
<div class=""main-bd"">
<h2><a href=""https://movie.douban.com/review/10179418/"">。。</a></h2>
<div id=""review_10179418_short"" class=""review-short"" data-rid=""10179418"">
<div class=""short-content"">
原以为是悬疑，没想到是思想教育片，教你树立正确的金钱观。虽然BGM很煽情，但是实在煽不起来……但是高桥一生这个角色，好！演技，好！这个角色是真的有点感人。坐在沙漠里讲落语的样子太动人。至于两男主这神奇的默契和信任，只能说这真的是小说，否则怎么可能有高桥一生那样...
 (<a href=""javascript:;"" id=""toggle-10179418-copy"" class=""unfold"" title=""展开"">展开</a>)
</div>
</div>
<div id=""review_10179418_full"" class=""hidden"">
<div id=""review_10179418_full_content"" class=""full-content""></div>
</div>
<div class=""action"">
<a href=""javascript:;"" class=""action-btn up"" data-rid=""10179418"" title=""有用"">
<img src=""https://img3.doubanio.com/f/zerkalo/536fd337139250b5fb3cf9e79cb65c6193f8b20b/pics/up.png"" />
<span id=""r-useful_count-10179418"">
</span>
</a>
<a href=""javascript:;"" class=""action-btn down"" data-rid=""10179418"" title=""没用"">
<img src=""https://img3.doubanio.com/f/zerkalo/68849027911140623cf338c9845893c4566db851/pics/down.png"" />
<span id=""r-useless_count-10179418"">
</span>
</a>
<a href=""https://movie.douban.com/review/10179418/#comments"" class=""reply "">0回应</a>
<a href=""javascript:;;"" class=""fold hidden"">收起</a>
</div>
</div>
</div>
</div>
<div data-cid=""10177242"">
<div class=""main review-item"" id=""10177242"">
<header class=""main-hd"">
<a href=""https://www.douban.com/people/162755192/"" class=""avator"">
<img width=""24"" height=""24"" src=""https://img1.doubanio.com/icon/u162755192-7.jpg"">
</a>
<a href=""https://www.douban.com/people/162755192/"" class=""name"">木召迩</a>
<span class=""allstar40 main-title-rating"" title=""推荐""></span>
<span content=""2019-05-13"" class=""main-meta"">2019-05-13 10:47:30</span>
</header>
<div class=""main-bd"">
<h2><a href=""https://movie.douban.com/review/10177242/"">你到底在为什么赚钱？</a></h2>
<div id=""review_10177242_short"" class=""review-short"" data-rid=""10177242"">
<div class=""short-content"">
<p class=""spoiler-tip"">这篇影评可能有剧透</p>
九十九凭着投资股票幸运得来的一亿去创业，追逐他的梦想。他想赚更多更多的钱，把生意做大做强。公司在他的带领下，发展迅猛。后来大公司看中了九十九的公司具有的发展潜力，将公司收购了。 起初九十九不同意被收购，毕竟谁也不想自己苦心经营多年的心血有朝一日被他人占领。但...
 (<a href=""javascript:;"" id=""toggle-10177242-copy"" class=""unfold"" title=""展开"">展开</a>)
</div>
</div>
<div id=""review_10177242_full"" class=""hidden"">
<div id=""review_10177242_full_content"" class=""full-content""></div>
</div>
<div class=""action"">
<a href=""javascript:;"" class=""action-btn up"" data-rid=""10177242"" title=""有用"">
<img src=""https://img3.doubanio.com/f/zerkalo/536fd337139250b5fb3cf9e79cb65c6193f8b20b/pics/up.png"" />
<span id=""r-useful_count-10177242"">
3
</span>
</a>
<a href=""javascript:;"" class=""action-btn down"" data-rid=""10177242"" title=""没用"">
<img src=""https://img3.doubanio.com/f/zerkalo/68849027911140623cf338c9845893c4566db851/pics/down.png"" />
<span id=""r-useless_count-10177242"">
</span>
</a>
<a href=""https://movie.douban.com/review/10177242/#comments"" class=""reply "">0回应</a>
<a href=""javascript:;;"" class=""fold hidden"">收起</a>
</div>
</div>
</div>
</div>
<!-- COLLECTED CSS -->
</div>
<p class=""pl"">
>
<a href=""reviews"">
更多影评11篇
</a>
</p>
</section>
<!-- COLLECTED JS -->
<br/>
<div class=""section-discussion"">
<div class=""mod-hd"">
<a class=""comment_btn j a_show_login"" href=""https://www.douban.com/register?reason=review"" rel=""nofollow""><span>添加新讨论</span></a>
<h2>
讨论区
  ·  ·  ·  ·  ·  ·
</h2>
</div>
<table class=""olt""><tr><td><td><td><td></tr>
<tr>
<td class=""pl""><a href=""https://movie.douban.com/subject/27604258/discussion/615900227/"" title=""可以看了么盆友们？！"">可以看了么盆友们？！</a></td>
<td class=""pl""><span>来自</span><a href=""https://www.douban.com/people/Lovelife-hh/"">太湖边的卡夫卡</a></td>
<td class=""pl""><span>9 回应</span></td>
<td class=""pl""><span>2019-05-21</span></td>
</tr>
<tr>
<td class=""pl""><a href=""https://movie.douban.com/subject/27604258/discussion/616304041/"" title=""这阵容竟然刚及格...."">这阵容竟然刚及格....</a></td>
<td class=""pl""><span>来自</span><a href=""https://www.douban.com/people/167905493/"">JimmieWong</a></td>
<td class=""pl""><span></span></td>
<td class=""pl""><span>2019-05-15</span></td>
</tr>
</table>
<p class=""pl"" align=""right"">
<a href=""/subject/27604258/discussion/"" rel=""nofollow"">
> 去这部影片的讨论区（全部2条）
</a>
</p>
</div>
</div>
<div class=""aside"">
<!-- douban ad begin -->
<div id=""dale_movie_subject_top_right""></div>
<div id=""dale_movie_subject_top_middle""></div>
<!-- douban ad end -->
<div class=""tags"">
<h2>
<i class="""">豆瓣成员常用的标签</i>
· · · · · ·
</h2>
<div class=""tags-body"">
<a href=""/tag/佐藤健"" class="""">佐藤健</a>
<a href=""/tag/日本"" class="""">日本</a>
<a href=""/tag/高桥一生"" class="""">高桥一生</a>
<a href=""/tag/日本电影"" class="""">日本电影</a>
<a href=""/tag/2018"" class="""">2018</a>
<a href=""/tag/日影"" class="""">日影</a>
<a href=""/tag/藤原龙也"" class="""">藤原龙也</a>
<a href=""/tag/小说改编"" class="""">小说改编</a>
</div>
</div>
<div id=""dale_movie_review_right_guess_you_like""></div>
<div id=""dale_movie_subject_inner_middle""></div>
<div id=""dale_movie_subject_download_middle""></div>
<div id=""subject-doulist"">
<h2>
<i class="""">以下豆列推荐</i>
· · · · · ·
<span class=""pl"">
(
<a href=""https://movie.douban.com/subject/27604258/doulists"">全部</a>
)
</span>
</h2>
<ul>
<li>
<a href=""https://www.douban.com/doulist/44585567/"" target=""_blank"">严重收藏癖患者的网盘资源清单，病友进！！</a>
<span>(秀山鲤鱼)</span>
</li>
<li>
<a href=""https://www.douban.com/doulist/44527893/"" target=""_blank"">2018年日本电影</a>
<span>(红色鲱鱼)</span>
</li>
<li>
<a href=""https://www.douban.com/doulist/42090855/"" target=""_blank"">高桥一生爱护协会剧单</a>
<span>(やす)</span>
</li>
<li>
<a href=""https://www.douban.com/doulist/1659872/"" target=""_blank"">小说改编</a>
<span>(木木木小锦)</span>
</li>
<li>
<a href=""https://www.douban.com/doulist/39140328/"" target=""_blank"">让我感谢你，赠与我的欢喜，我不会忘记</a>
<span>(苏)</span>
</li>
</ul>
</div>
<div id=""subject-others-interests"">
<h2>
<i class="""">谁在看这部电影</i>
· · · · · ·
</h2>
<ul class="""">
<li class="""">
<a href=""https://www.douban.com/people/181420014/"" class=""others-interest-avatar"">
<img src=""https://img3.doubanio.com/icon/u181420014-1.jpg"" class=""pil"" alt=""Taylor swift"">
</a>
<div class=""others-interest-info"">
<a href=""https://www.douban.com/people/181420014/"" class="""">Taylor swift</a>
<div class="""">
今天凌晨
看过
<span class=""allstar30"" title=""还行""></span>
</div>
</div>
</li>
<li class="""">
<a href=""https://www.douban.com/people/Missy_Linger/"" class=""others-interest-avatar"">
<img src=""https://img1.doubanio.com/icon/u4468010-247.jpg"" class=""pil"" alt=""克柔"">
</a>
<div class=""others-interest-info"">
<a href=""https://www.douban.com/people/Missy_Linger/"" class="""">克柔</a>
<div class="""">
今天凌晨
看过
</div>
</div>
</li>
<li class="""">
<a href=""https://www.douban.com/people/44641964/"" class=""others-interest-avatar"">
<img src=""https://img3.doubanio.com/icon/u44641964-10.jpg"" class=""pil"" alt=""maladona1960"">
</a>
<div class=""others-interest-info"">
<a href=""https://www.douban.com/people/44641964/"" class="""">maladona1960</a>
<div class="""">
今天凌晨
看过
<span class=""allstar30"" title=""还行""></span>
</div>
</div>
</li>
</ul>
<div class=""subject-others-interests-ft"">
<a href=""https://movie.douban.com/subject/27604258/collections"">1089人看过</a>
 / 
<a href=""https://movie.douban.com/subject/27604258/wishes"">1597人想看</a>
</div>
</div>
<!-- douban ad begin -->
<div id=""dale_movie_subject_middle_right""></div>
<!-- douban ad end -->
<br/>
<p class=""pl"">订阅亿男的评论: <br/><span class=""feed"">
<a href=""https://movie.douban.com/feed/subject/27604258/reviews""> feed: rss 2.0</a></span></p>
</div>
<div class=""extra"">
<!-- douban ad begin -->
<div id=""dale_movie_subject_bottom_super_banner""></div>
<!-- douban ad end -->
</div>
</div>
</div>
<div id=""footer"">
<div class=""footer-extra""></div>
<span id=""icp"" class=""fleft gray-link"">
© 2005－2019 douban.com, all rights reserved 北京豆网科技有限公司
</span>
<a href=""https://www.douban.com/hnypt/variformcyst.py"" style=""display: none;""></a>
<span class=""fright"">
<a href=""https://www.douban.com/about"">关于豆瓣</a>
· <a href=""https://www.douban.com/jobs"">在豆瓣工作</a>
· <a href=""https://www.douban.com/about?topic=contactus"">联系我们</a>
· <a href=""https://www.douban.com/about/legal"">法律声明</a>
· <a href=""https://help.douban.com/?app=movie"" target=""_blank"">帮助中心</a>
· <a href=""https://www.douban.com/doubanapp/"">移动应用</a>
· <a href=""https://www.douban.com/partner/"">豆瓣广告</a>
</span>
</div>
</div>
<!-- brand59-docker-->
</body>
</html>
",
                DoubanUrl = "https://movie.douban.com/subject/27604258/",
                DyUrl = "https://www.dytt8.net//html/gndy/dyzz/20190524/58631.html",
                ImgData = "data:image/jpg;base64,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",
                MovieName = "亿男",
                Resoures = "ftp://ygdy8:ygdy8@yg90.dydytt.net:8538/阳光电影www.ygdy8.com.亿男.BD.720p.日语中字.mkv",
                Status = Common.Enums.MovieDraftStatusEnum.Unverified,
            };
            dbContext.MovieDrafts.Add(movieDraft);
            dbContext.SaveChanges();
            var movieAreaService = new Mock<IMovieAreaService>();
            movieAreaService.Setup(s => s.GetMovieAreaId(It.IsAny<string>())).ReturnsAsync(1);
            var movieDraftService = new MovieDraftService(mapper, dbContext, movieAreaService.Object, null);
            //Act
            var result = await movieDraftService.GetMovieDraftDetail(movieDraft.Id);
            //Assert
            Assert.True(result == null);
        }
    }
}
