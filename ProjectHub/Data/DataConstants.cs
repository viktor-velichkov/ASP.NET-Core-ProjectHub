namespace ProjectHub.Data
{
    public class DataConstants
    {
        public const string DefaultConnectionString = "Server =.; Database = ProjectHub; Trusted_Connection = True;";

        public const int UserFirstNameMinLength = 3;
        public const int UserFirstNameMaxLength = 20;

        public const int UserLastNameMinLength = 3;
        public const int UserLastNameMaxLength = 20;

        public const int UserKindNameMinLength = 3;
        public const int UserKindNameMaxLength = 20;

        public const int PasswordMinLength = 8;
        public const int PasswordMaxLength = 20;

        public const int WebSiteAddresMinLength = 7;
        public const int WebSiteAddresMaxLength = 50;

        public const int DescriptionMinLength = 8;
        public const int DescriptionMaxLength = 10000;

        public const int ContentMinLength = 8;
        public const int ContentMaxLength = 10000;

        public const int ProjectNameMinLength = 10;
        public const int ProjectNameMaxLength = 150;

        public const int ProjectCityMinLength = 3;
        public const int ProjectCityMaxLength = 50;

        public const int ProjectAddresMinLength = 10;
        public const int ProjectAddresMaxLength = 300;

        public const int OfferNameMinLength = 10;
        public const int OfferNameMaxLength = 150;
    }
}
