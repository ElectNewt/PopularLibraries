namespace PopularLibraries.Tests.Mock;

public class ArticleService
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IArticleRepository _articleRepository;

    public ArticleService(IAuthorRepository authorRepository, IArticleRepository articleRepository)
    {
        _authorRepository = authorRepository;
        _articleRepository = articleRepository;
    }

    public Article InsertArticle(string content, string title, int authorId)
    {
        if (!_authorRepository.IsValid(authorId))
        {
            throw new Exception("Author not valid");
        }
            
            
        int articleId = _articleRepository.Insert(content, title, authorId);

        return GetArticle(articleId);
    }

    public Article GetArticle(int id)
    {
        return _articleRepository.Get(id);
    }
}


public interface IAuthorRepository
{
    Author Get(int id);
    bool IsValid(int id);
}
public interface IArticleRepository
{
    int Insert(string content, string title, int authorId);
    Article Get(int id);
}

public record Article(int Id, string Content, string Title, DateOnly Date, int AuthorId);

public record Author(int Id, string Name);
