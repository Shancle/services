using System.Runtime.Serialization;

namespace LibraryService.Data
{
    [DataContract]
    public enum BookType
    {
        [EnumMember] FictionBook,
        [EnumMember] Journal,
        [EnumMember] ResearchArticle,
        [EnumMember] Chronicle
    }
}