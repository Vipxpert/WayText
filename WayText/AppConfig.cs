
namespace Emoji
{
    internal class AppConfig
    {
        public bool showHint { get; set; }
        public bool startOnBoot { get; set; }
        public bool hideOnBoot { get; set; }
        public string folderDelimiter { get; set; }
        public string excludeFolderDelimiter { get; set; }
        public string dataDelimiter { get; set; }
        public int maxNumberOfColumnVisible { get; set; }
        public int formHeight { get; set; }
        public string [][] excludedFolderFromGroup { get; set; }
        public string[][] includedFolderInGroup { get; set; }
       public List<string> groupType { get; set; }
        public bool headerExpand { get; set; }
        public bool []headerGroupExpand { get; set; }
        public int startTypeIndex { get; set; }
        public int startCategoryIndex { get; set; }
        public bool actionInstantCopy { get; set; }
        public string path { get; set; }
        public string [] hotkeyShowHideApp { get; set; }
        public string[][] hotkeyChangeTypeCategory { get; set; }
        public int numberOfScroll { get; set; }

    }
}
