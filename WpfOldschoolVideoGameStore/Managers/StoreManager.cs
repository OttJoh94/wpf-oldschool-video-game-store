using System.Collections.Generic;
using WpfOldschoolVideoGameStore.Models;

namespace WpfOldschoolVideoGameStore.Managers
{
    public static class StoreManager
    {
        public static IUser? SignedInUser { get; set; }
        public static List<IUser> Users { get; set; } = new()
        {
            new Admin() {Username = "admin", Password = "123"},
            new Customer() {Username = "customer", Password = "123", IsAbove18 = true }
        };


        public static List<IMedia> Medias { get; set; } = new()
        {
            new Movie() {Name = "Lord of the Rings", Genre = "Fantasy", Rating = 10, IsRRated = false, IsRentedOut = false},
            new Game() {Name = "Doom", Genre = "FPS" , Rating = 8, IsRentedOut = false, IsRRated = true}
        };

    }
}
