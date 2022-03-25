using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceApp.Models
{
	public class Article
	{

		private static readonly char _separator = ';';
		private string TagsCollection;

		public int Id { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public int AuthorId { get; set; }
		[Column(TypeName = "Date")]
		public DateTime LastEditDate { get; set; }
		public string fileName { get; set; }

		[NotMapped]
		public string[] Tags
        {
			get => TagsCollection.Split(_separator);
			set => TagsCollection = string.Join($"{_separator}", value);
        }

	}
}

