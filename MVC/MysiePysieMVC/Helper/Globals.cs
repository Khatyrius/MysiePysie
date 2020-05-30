namespace MysiePysieMVC.Helper
{
    public static class Globals
    {
        public const string API_LINK = "http://localhost:8080/";
        public const string STUDENTS_API_LINK = "students";
        public const string TEACHERS_API_LINK = "teachers";
        public const string SUBJECTS_API_LINK = "subjects";
        public const string USER_API_LINK = "users";
        public const string USER_VALIDATE_API_LINK = "users/login";
        public const string CLASSES_API_LINK = "classes";
        public const string CLASSES_ADD_API_LINK = "classes/add";
        public const string CLASSES_REMOVE_API_LINK = "classes/remove";
        public const int ADMIN = 666;
        public const int ACVITE = 1;
        public const int INACTIVe = 0;

        public static string Token { get; set; }
    }
}