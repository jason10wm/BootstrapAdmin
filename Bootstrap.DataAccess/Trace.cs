﻿using Longbow.Web.Mvc;
using PetaPoco;
using System;

namespace Bootstrap.DataAccess
{
    /// <summary>
    /// 
    /// </summary>
    public class Trace
    {
        /// <summary>
        /// 获得/设置 操作日志主键ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 获得/设置 用户名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 获得/设置 操作时间
        /// </summary>
        public DateTime LogTime { get; set; }

        /// <summary>
        /// 获得/设置 客户端IP
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Browser { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OS { get; set; }

        /// <summary>
        /// 获取/设置 请求网址
        /// </summary>
        public string RequestUrl { get; set; }

        /// <summary>
        /// 保存访问历史记录
        /// </summary>
        /// <param name="p"></param>
        public virtual bool Save(Trace p)
        {
            if (p == null) throw new ArgumentNullException(nameof(p));
            DbManager.Create().Save(p);
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public virtual Page<Trace> Retrieves(PaginationOption po, DateTime? startTime, DateTime? endTime)
        {
            var sql = new Sql("select * from Traces");
            if (startTime.HasValue) sql.Append("where LogTime > @0", startTime.Value);
            if (endTime.HasValue) sql.Append("where LogTime < @0", endTime.Value.AddDays(1).AddSeconds(-1));
            if (startTime == null && endTime == null) sql.Append("where LogTime > @0", DateTime.Today.AddDays(-7));
            sql.Append($"order by {po.Sort} {po.Order}");

            return DbManager.Create().Page<Trace>(po.PageIndex, po.Limit, sql);
        }
    }
}