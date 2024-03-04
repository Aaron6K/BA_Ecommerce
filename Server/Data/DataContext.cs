namespace BA_Ecommerce.Server.Data
{
   public class DataContext:DbContext
   {
      public DataContext(DbContextOptions<DataContext> options):base(options) { 


      }
      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         modelBuilder.Entity<Product>().HasData(
            new Product
            {
               Id = 1,
               Title = "The Hitchhiker's Guide to the Galaxy",
               Description = "The Hitchhiker's Guide to the Galaxy is an international multimedia phenomenon; the novels are the most widely distributed, having been translated into more than 30 languages by 2005.[4][5] The first novel, The Hitchhiker's Guide to the Galaxy (1979), has been ranked fourth on the BBC's The Big Read poll.[6] The sixth novel, And Another Thing..., was written by Eoin Colfer with additional unpublished material by Douglas Adams. In 2017, BBC Radio 4 announced a 40th-anniversary celebration with Dirk Maggs, one of the original producers, in charge.[7] The first of six new episodes was broadcast on 8 March 2018.[8]",
               ImageUrl = "https://upload.wikimedia.org/wikipedia/en/b/bd/H2G2_UK_front_cover.jpg",
               Price = 9.99m
            },
            new Product
            {
               Id = 2,
               Title = "Ready Player One (film)",
               Description = "Ready Player One is a 2018 American science fiction action film based on Ernest Cline's novel of the same name. The film was co-produced and directed by Steven Spielberg, written by Cline and Zak Penn, and stars Tye Sheridan, Olivia Cooke, Ben Mendelsohn, Lena Waithe, T.J. Miller, Simon Pegg, and Mark Rylance. The film is set in 2045, where much of humanity uses the OASIS, a virtual reality simulation, to escape the real world. A teenage orphan finds clues to a contest that promises ownership of the OASIS to the winner, and he and his allies try to complete it before an evil corporation can do so.",
               ImageUrl = "https://upload.wikimedia.org/wikipedia/en/7/74/Ready_Player_One_%28film%29.png",
               Price = 7.99m
            },
            new Product
            {
               Id = 3,
               Title = "Nineteen Eighty-Four",
               Description = "Nineteen Eighty-Four (also published as 1984) is a dystopian novel and cautionary tale by English writer George Orwell. It was published on 8 June 1949 by Secker & Warburg as Orwell's ninth and final book completed in his lifetime. Thematically, it centres on the consequences of totalitarianism, mass surveillance and repressive regimentation of people and behaviours within society.[3][4] Orwell, a democratic socialist, modelled the authoritarian state in the novel on the Soviet Union in the era of Stalinism, and Nazi Germany.[5] More broadly, the novel examines the role of truth and facts within societies and the ways in which they can be manipulated.",
               ImageUrl = "https://upload.wikimedia.org/wikipedia/en/5/51/1984_first_edition_cover.jpg",
               Price = 6.99m
            }
         );
      }

      public DbSet<Product> Products { get; set; }

     
   }
}
