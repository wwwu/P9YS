using P9YS.EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace P9YS.EntityFramework
{
    public static class SeedData
    {
        public static MovieArea[] GetDefaultMoiveAreas()
        {
            var time = DateTime.Now;
            var id = 1;
            return new MovieArea[]
            {
                new MovieArea{ Id=id++,Area="中国",Other=0,AddTime=time },
                new MovieArea{ Id=id++,Area="香港",Other=0,AddTime=time },
                new MovieArea{ Id=id++,Area="台湾",Other=0,AddTime=time },
                new MovieArea{ Id=id++,Area="美国",Other=0,AddTime=time },
                new MovieArea{ Id=id++,Area="英国",Other=0,AddTime=time },
                new MovieArea{ Id=id++,Area="日本",Other=0,AddTime=time },
                new MovieArea{ Id=id++,Area="韩国",Other=0,AddTime=time },
                new MovieArea{ Id=id++,Area="法国",Other=0,AddTime=time },
                new MovieArea{ Id=id++,Area="印度",Other=1,AddTime=time },
                new MovieArea{ Id=id++,Area="泰国",Other=1,AddTime=time },
                new MovieArea{ Id=id++,Area="加拿大",Other=1,AddTime=time },
                new MovieArea{ Id=id++,Area="澳大利亚",Other=1,AddTime=time },
                new MovieArea{ Id=id++,Area="俄罗斯",Other=1,AddTime=time },
                new MovieArea{ Id=id++,Area="伊朗",Other=1,AddTime=time },
                new MovieArea{ Id=id++,Area="爱尔兰",Other=1,AddTime=time },
                new MovieArea{ Id=id++,Area="瑞典",Other=1,AddTime=time },
                new MovieArea{ Id=id++,Area="丹麦",Other=1,AddTime=time },
                new MovieArea{ Id=id++,Area="巴西",Other=1,AddTime=time },
                new MovieArea{ Id=id++,Area="墨西哥",Other=1,AddTime=time },
                new MovieArea{ Id=id++,Area="新西兰",Other=1,AddTime=time },
                new MovieArea{ Id=id++,Area="新加坡",Other=1,AddTime=time },
                new MovieArea{ Id=id++,Area="土耳其",Other=1,AddTime=time },
                new MovieArea{ Id=id++,Area="捷克",Other=1,AddTime=time },
                new MovieArea{ Id=id++,Area="波兰",Other=1,AddTime=time },
                new MovieArea{ Id=id++,Area="阿根廷",Other=1,AddTime=time },
                new MovieArea{ Id=id++,Area="奥地利",Other=1,AddTime=time },
                new MovieArea{ Id=id++,Area="比利时",Other=1,AddTime=time },
                new MovieArea{ Id=id++,Area="其它",Other=1,AddTime=time },
            };
        }

        public static MovieGenre[] GetDefaultMovieGenres()
        {
            var time = DateTime.Now;
            var id = 1;
            return new MovieGenre[]
            {
                new MovieGenre(){Id=id++,Name="科幻",AddTime=time },
                new MovieGenre(){Id=id++,Name="动作",AddTime=time },
                new MovieGenre(){Id=id++,Name="犯罪",AddTime=time },
                new MovieGenre(){Id=id++,Name="战争",AddTime=time },
                new MovieGenre(){Id=id++,Name="剧情",AddTime=time },
                new MovieGenre(){Id=id++,Name="青春",AddTime=time },
                new MovieGenre(){Id=id++,Name="爱情",AddTime=time },
                new MovieGenre(){Id=id++,Name="烂片",AddTime=time },
                new MovieGenre(){Id=id++,Name="文艺",AddTime=time },
                new MovieGenre(){Id=id++,Name="喜剧",AddTime=time },
                new MovieGenre(){Id=id++,Name="音乐",AddTime=time },
                new MovieGenre(){Id=id++,Name="动画",AddTime=time },
                new MovieGenre(){Id=id++,Name="童话",AddTime=time },
                new MovieGenre(){Id=id++,Name="奇幻",AddTime=time },
                new MovieGenre(){Id=id++,Name="史诗",AddTime=time },
                new MovieGenre(){Id=id++,Name="家庭",AddTime=time },
                new MovieGenre(){Id=id++,Name="暴力",AddTime=time },
                new MovieGenre(){Id=id++,Name="恐怖",AddTime=time },
                new MovieGenre(){Id=id++,Name="同性",AddTime=time },
                new MovieGenre(){Id=id++,Name="悬疑",AddTime=time },
                new MovieGenre(){Id=id++,Name="缉毒",AddTime=time },
                new MovieGenre(){Id=id++,Name="黑帮",AddTime=time },
                new MovieGenre(){Id=id++,Name="卧底",AddTime=time },
                new MovieGenre(){Id=id++,Name="枪战",AddTime=time },
                new MovieGenre(){Id=id++,Name="宇宙",AddTime=time },
                new MovieGenre(){Id=id++,Name="星战",AddTime=time },
                new MovieGenre(){Id=id++,Name="武侠",AddTime=time },
                new MovieGenre(){Id=id++,Name="纪录",AddTime=time },
                new MovieGenre(){Id=id++,Name="监狱",AddTime=time },
                new MovieGenre(){Id=id++,Name="历史",AddTime=time },
                new MovieGenre(){Id=id++,Name="传记",AddTime=time },
            };
        }

    }
}
