using FakeItEasy;

namespace PopularLibraries.Tests.Mock;

public class ExampleFakeItEasy
{
    [Fact]
    public void WhenArticleExist_ThenReturnArticle() //FakeIt easy
    {
        Article article = new Article(1, "content", "title", DateOnly.FromDateTime(DateTime.UtcNow), 1);

        IAuthorRepository authorRepository = A.Fake<IAuthorRepository>();
        IArticleRepository articleRepository = A.Fake<IArticleRepository>();
        A.CallTo(() => articleRepository.Get(article.Id)).Returns(article);

        ArticleService service = new ArticleService(authorRepository, articleRepository);
        Article result = service.GetArticle(article.Id);

        Assert.Equal(article.Content, result.Content);
    }

    [Fact]
    public void WhenAuthorIsValid_ThenArticleIsInserted()
    {
        Article article = new Article(1, "content", "title", DateOnly.FromDateTime(DateTime.UtcNow), 10);

        IAuthorRepository authorRepository = A.Fake<IAuthorRepository>();
        IArticleRepository articleRepository = A.Fake<IArticleRepository>();
        A.CallTo(() => authorRepository.IsValid(A<int>._)).Returns(true);
        A.CallTo(() => articleRepository.Insert(article.Content, article.Title, article.AuthorId)).Returns(article.Id);
        A.CallTo(() => articleRepository.Get(article.Id)).Returns(article);

        ArticleService service = new ArticleService(authorRepository, articleRepository);
        Article result = service.InsertArticle(article.Content, article.Title, article.AuthorId);

        A.CallTo(() => articleRepository.Insert(article.Content, article.Title, article.AuthorId))
            .MustHaveHappened(1, Times.Exactly);
        Assert.Equal(article.Content, result.Content);
    }
}