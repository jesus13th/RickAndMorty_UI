using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using RickAndMorty.Models;

namespace RickAndMorty.Services {
    internal class RickAndMortyService {
        private static RickAndMortyService instance;
        public static RickAndMortyService Instance {
            get {
                if (instance == null) {
                    instance = new RickAndMortyService();
                }
                return instance;
            }
            private set { }
        }
        const string URL = "https://rickandmortyapi.com/api/character/?page=";
        public uint counter = 1;
        public async Task<Characters> GetCharacters() {
            var client = new HttpClient();
            var response = await client.GetAsync(URL+counter);
            var content = await response.Content.ReadAsStringAsync();
            var characters = JsonSerializer.Deserialize<Characters>(content);

            return characters;
        }
    }
}
