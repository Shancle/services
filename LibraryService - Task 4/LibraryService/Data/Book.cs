using System.Runtime.Serialization;

namespace LibraryService.Data
{
    [DataContract]
    public class Book
    {
        public Book(int id, string name, string author, int publicationYear, BookType bookType, bool isInLibrary = true)
        {
            Id = id;
            Name = name;
            Author = author;
            PublicationYear = publicationYear;
            BookType = bookType;
            IsInLibrary = isInLibrary;
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Author { get; set; }

        [DataMember]
        public int PublicationYear { get; set; }

        [DataMember]
        public BookType BookType { get; set; }

        public bool IsInLibrary { get; set; }
    }
}