using NSubstitute;
using NSubstitute.ReceivedExtensions;

namespace PopularLibraries.Tests.Mock;

public class ExampleNSubstitute
{
    [Fact]
    public void WhenArticleExist_ThenReturnArticle() //Nsubstitute
    {
        Article article = new Article(1, "content", "title", DateOnly.FromDateTime(DateTime.UtcNow), 1);

        IAuthorRepository authorRepository = Substitute.For<IAuthorRepository>();
        IArticleRepository articleRepository = Substitute.For<IArticleRepository>();
        articleRepository.Get(article.Id).Returns(article);
        
        ArticleService service = new ArticleService(authorRepository, articleRepository);
        Article result = service.GetArticle(article.Id);

        Assert.Equal(article.Content, result.Content);
    }


    // [Fact]
    // public void WhenAuthorIsValid_ThenArticleIsInserted()
    // {
    //     Article article = new Article(1, "content", "title", DateOnly.FromDateTime(DateTime.UtcNow), 10);
    //
    //     IAuthorRepository authorRepository = Substitute.For<IAuthorRepository>();
    //     IArticleRepository articleRepository = Substitute.For<IArticleRepository>();
    //     authorRepository.IsValid(article.AuthorId).Returns(true);
    //     articleRepository.Insert(article.Content, article.Title, article.AuthorId)
    //         .;
    //     articleRepository.Get(article.Id).Returns(article);
    //     
    //     ArticleService service = new ArticleService(authorRepository, articleRepository);
    //     Article result = service.InsertArticle(article.Content, article.Title, article.AuthorId);
    //
    //     Assert.Equal(article.Content, result.Content);
    // }
    
    [Fact]
    public void WhenAuthorIsValid_ThenArticleIsInserted()
    {
        Article article = new Article(1, "content", "title", DateOnly.FromDateTime(DateTime.UtcNow), 10);

        IAuthorRepository authorRepository = Substitute.For<IAuthorRepository>();
        IArticleRepository articleRepository = Substitute.For<IArticleRepository>();
        articleRepository.Get(article.Id).Returns(article);
        authorRepository.IsValid(Arg.Any<int>()).Returns(true);
        articleRepository.Insert(article.Content, article.Title, article.AuthorId).Returns(article.Id);
    
        ArticleService service = new ArticleService(authorRepository, articleRepository);
        Article result = service.InsertArticle(article.Content, article.Title, article.AuthorId);

        articleRepository.Received(1).Insert(article.Content, article.Title, article.AuthorId);
        Assert.Equal(article.Content, result.Content);
    }
}