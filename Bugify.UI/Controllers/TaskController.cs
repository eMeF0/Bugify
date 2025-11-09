using System.Text;
using System.Text.Json;
using Bugify.UI.Models;
using Bugify.UI.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Bugify.UI.Controllers
{
    public class TaskController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TaskController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<TaskDto> response = new List<TaskDto>();
            try
            {
                var client = _httpClientFactory.CreateClient();

                var httpResponse = await client.GetAsync("https://localhost:7148/api/Task");

                httpResponse.EnsureSuccessStatusCode();

                response.AddRange(await httpResponse.Content.ReadFromJsonAsync<IEnumerable<TaskDto>>());
            }
            catch (Exception e)
            {
            }

            return View(response);

        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateTaskViewModel createTaskDto)
        {
            var client = _httpClientFactory.CreateClient();

            var httpRequest = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7148/api/Task"),
                Content = new StringContent(JsonSerializer.Serialize(createTaskDto), Encoding.UTF8, "application/json")
            };

            var httpResponse = await client.SendAsync(httpRequest);
            httpResponse.EnsureSuccessStatusCode();
            var response = await httpResponse.Content.ReadFromJsonAsync<TaskDto>();

            if (response is not null)
            {
                return RedirectToAction("Index", "Task");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.GetFromJsonAsync<TaskDto>($"https://localhost:7148/api/Task/{id.ToString()}");
            if (response is not null)
            {
                return View(response);
            }
            return View(null);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(TaskDto taskDto)
        {
            var client = _httpClientFactory.CreateClient();

            var httpRequest = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7148/api/Task/{taskDto.Id}"),
                Content = new StringContent(JsonSerializer.Serialize(taskDto), Encoding.UTF8, "application/json")
            };
            var httpResponse = await client.SendAsync(httpRequest);
            httpResponse.EnsureSuccessStatusCode();
            var response = await httpResponse.Content.ReadFromJsonAsync<TaskDto>();
            if (response is not null)
            {
                return RedirectToAction("Edit", "Task");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(TaskDto taskDto)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var httpResponse = await client.DeleteAsync($"https://localhost:7148/api/Task/{taskDto.Id}");
                httpResponse.EnsureSuccessStatusCode();
                return RedirectToAction("Index", "Task");
            }
            catch (Exception e)
            {
            }
            return View("Edit");
        }
    }
}
