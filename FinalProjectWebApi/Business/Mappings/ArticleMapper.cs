using FinalProjectWebApi.Entities.Abstract;
using FinalProjectWebApi.Entities.Concrete;
using Riok.Mapperly.Abstractions;

namespace FinalProjectWebApi.Business.Mappings
{
    [Mapper]
    public partial class ArticleMapper
    {
        public partial ArticleDto MapToDto(Article article);
        public partial Article MapToEntity(ArticleDto articleDto);
    }
}
