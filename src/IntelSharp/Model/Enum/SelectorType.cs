namespace IntelSharp.Model
{
    /// <summary>
    /// Represents all the possible selector types to be either used as search term in <see cref="IntelligentSearchApi"/> or exported using <see cref="ItemApi.ListSelectorsAsync(Item, System.Threading.CancellationToken)"/>.
    /// </summary>
    public enum SelectorType
    {
        UUID = 0,
        Email,
        Domain,
        URL,
        Phone,
        Person,
        IP,
        CIDR,
        User,
        Address,
        Tax,
        SocialNumber,
        IdentityNumber,
        CompanyNumber,
        SocialMedia,
        Company,
        BankNumber,
        CreditCard,
        File,
        StorageId,
        GS1GLN,
        GS1GTIN,
        MAC,
        URLQuery,
        Simhash,
        Bitcoin,
        Emoji,
        IPFSContentId
    }
}
