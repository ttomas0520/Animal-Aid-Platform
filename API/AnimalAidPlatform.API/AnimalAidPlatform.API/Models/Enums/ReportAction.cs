namespace AnimalAidPlatform.API.Models.Enums
{
    using System.Runtime.Serialization;
    public enum ReportAction
    {
        [EnumMember(Value = "None")]
        None,

        [EnumMember(Value = "Delete")]
        Delete,

        [EnumMember(Value = "Notice Creator")]
        NoticeCreator
    }
}
