using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

/// <summary>
/// 备注特性
/// </summary>
public class RemarkAttribute : Attribute
{
    private string m_remark;
    public RemarkAttribute(string remark)
    {
        m_remark = remark;
    }
    /// <summary>
    /// 备注
    /// </summary>
    public string Remark
    {
        get { return m_remark; }
        set { m_remark = value; }
    }
    /// <summary>
    /// 获取枚举的备注信息
    /// </summary>
    /// <param name="val">枚举值</param>
    /// <returns></returns>
    public static string GetEnumRemark(Enum val)
    {
        Type type = val.GetType();
        FieldInfo fd = type.GetField(val.ToString());
        if (fd == null)
            return string.Empty;
        object[] attrs = fd.GetCustomAttributes(typeof(RemarkAttribute), false);
        string name = string.Empty;
        foreach (RemarkAttribute attr in attrs)
        {
            name = attr.Remark;
        }
        return name;
    }
}
public static class EnumExt
{
    /// <summary>
    /// 获取枚举的备注信息
    /// </summary>
    /// <param name="em"></param>
    /// <returns></returns>
    public static string GetRemark(this Enum em)
    {
        string value = em.ToString();
        FieldInfo field = em.GetType().GetField(value);
        object[] objs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);    //获取描述属性
        if (objs.Length == 0)    //当描述属性没有时，直接返回名称
            return value;
        DescriptionAttribute descriptionAttribute = (DescriptionAttribute)objs[0];
        return descriptionAttribute.Description;
    }

    public static string GetRemark<T>(int i)
    {
        Type tp = typeof(T);
        if (Enum.IsDefined(tp, i))
        {
            Enum a = (Enum)Enum.ToObject(tp, i);
            return a.GetRemark();
        }
        return null;
    }

    public static string GetRemark<T>(int? i)
    {
        if (i == null)
        {
            return null;
        }
        return GetRemark<T>(i ?? 0);
    }
    /// <summary>
    /// 获取 枚举值与备注字典
    /// </summary>
    /// <param name="tp"></param>
    /// <returns></returns>
    public static Dictionary<int, string> GetEnumDic(this Type tp)
    {
        Dictionary<int, string> dic = new Dictionary<int, string>();
        var names = Enum.GetNames(tp);
        foreach (var item in names)
        {
            var em = Enum.Parse(tp, item);
            var key = (int)em;
            var value = ((Enum)em).GetRemark();
            if (!dic.ContainsKey(key))
                dic.Add(key, value);
        }
        return dic;
    }
}
