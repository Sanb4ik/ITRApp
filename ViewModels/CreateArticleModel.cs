using System;
namespace ServiceApp.ViewModels
{
	public class CreateArticleModel
	{
		public string Title { get; set; }
		public string Content { get; set; }
		public int AuthorId { get; set; }
	}
}

