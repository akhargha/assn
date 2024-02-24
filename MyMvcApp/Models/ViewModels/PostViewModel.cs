using System.Collections.Generic;

namespace MyMvcApp.Models.ViewModels
{
    public class PostViewModel
    {
        public Post? Post { get; set; } // Nullable in case there's a need to handle posts without data
        public string DisplayName { get; set; } = "Unknown"; // Default to "Unknown" to handle cases where user data might not be available
        public int ReputationScore { get; set; } = -1;
        public List<string> Badges { get; set; } = new List<string>(); // Initializes to an empty list to avoid null reference issues

        // Depending on your needs, you might add additional properties here that could include
        // details like comments count, views count, etc., pulled from the Post object or related data.
    }
}
