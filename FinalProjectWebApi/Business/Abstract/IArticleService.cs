﻿using FinalProjectWebApi.Entities.Abstract;
using FinalProjectWebApi.Entities.Concrete;

namespace FinalProjectWebApi.Business.Abstract
{
    public interface IArticleService
    {

        Task<List<ArticleDto>> GetArticlesAsync();
        Task<PagingResult<ArticleDto>> GetArticlesPagedAsync(int pageNumber, int pageSize, int? categoryId, string? keyword, DateTime? minDate, DateTime? maxDate);
        Task<PagingResult<ArticleDto>> GetArticlesByUserIdAsync(int userId, int pageNumber, int pageSize, int? categoryId, string? keyword, DateTime? minDate, DateTime? maxDate);
        Task<Article> GetArticleByIdAsync(int id);
        Task<Article> AddArticleAsync(Article article);
        Task<bool> UpdateArticleAsync(int id, Article updatedArticle);
        Task<Article>DeleteArticleAsync(int id);
        Task<bool> DeleteArticlesAsync(List<int> articleIds);

    }
}
