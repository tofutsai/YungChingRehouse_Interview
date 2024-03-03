using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace YungChingRehouse_Interview.Models.DAL
{
    /// <summary>
    /// 代表一個Repository的interface。
    /// </summary>
    /// <typeparam name="T">任意model的class</typeparam>
    public interface IRepository<T>
    {
        /// <summary>
        /// 新增一筆資料。
        /// </summary>
        /// <param name="entity">要新增到的Entity</param>
        void Create(T entity);

        /// <summary>
        /// 取得第一筆符合條件的內容。如果符合條件有多筆，也只取得第一筆。
        /// </summary>
        /// <param name="predicate">要取得的Where條件。</param>
        /// <returns>取得第一筆符合條件的內容。</returns>
        T Read(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 取得該用戶Entity全部筆數的IQueryable。
        /// </summary>
        /// <param name="predicate">要取得的Where條件。</param>
        /// <returns>該用戶Entity全部筆數的IQueryable。</returns>
        IQueryable<T> Reads(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 更新一筆資料的內容。
        /// </summary>
        /// <param name="entity">要更新的內容</param>
        void Update(T entity);

        /// <summary>
        /// 更新一筆Entity的內容。只更新有指定的Property。
        /// </summary>
        /// <param name="entity">要更新的內容。</param>
        /// <param name="updateProperties">需要更新的欄位。</param>
        void Update(T entity, Expression<Func<T, object>>[] updateProperties);

        /// <summary>
        /// 刪除一筆資料內容。
        /// </summary>
        /// <param name="entity">要被刪除的Entity。</param>
        void Delete(T entity);

        /// <summary>
        /// 儲存異動。
        /// </summary>
        int SaveChanges();
    }
}
