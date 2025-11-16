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

                var data = await httpResponse.Content.ReadFromJsonAsync<IEnumerable<TaskDto>>();

                if (data is not null)
                {
                    response.AddRange(data);
                }
            }
            catch (Exception e)
            {
            }

            var boardModel = BuildBoardViewModel(response);

            return View(boardModel);

        }

        private TaskBoardPageViewModel BuildBoardViewModel(IEnumerable<TaskDto> tasks)
        {
            var columns = tasks.GroupBy(x => x.ProgressId).Select(x =>
            {
                var name = GetColumnName(x.Key);
                return new TaskColumnViewModel
                {
                    ProgressId = x.Key,
                    Name = name,
                    Tasks = x.OrderBy(x => x.DueDate ?? DateTime.MaxValue).Select(x => new TaskListViewModel
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Description = x.Description,
                        DueDate = x.DueDate,
                        ProgressId = x.ProgressId,
                        ProgressName = x.ProgressName
                    })
                    .ToList()
                };
            }).ToList();
            string[] columnOrder =
            {
                "NotStarted",
                "InProgress",
                "Completed",
                "OnHold",
                "Cancelled"
            };
            columns = columns.OrderBy(x =>
            {
                var index = Array.IndexOf(columnOrder, x.Name);
                return index >= 0 ? index : int.MaxValue;
            }).ToList();

            return new TaskBoardPageViewModel
            {
                Columns = columns
            };
        }

        private string GetColumnName(Guid ProgressId)
        {
            if(ProgressId == new Guid("759c64cb-9639-4218-bd6f-e68718429075"))
                return "NotStarted";
            if(ProgressId == new Guid("915ea79b-f8fa-4f1a-bf02-e9d73faf14a6"))
                return "InProgress";
            if(ProgressId == new Guid("852a80bb-a5d1-4ebd-8959-c115a0ddee68"))
                return "Completed";
            if(ProgressId == new Guid("c99a883d-51e3-4738-9ee6-c6ad53d73b37"))
                return "OnHold";
            if(ProgressId == new Guid("86704552-8451-4916-8494-b0dfd3490f44"))
                return "Cancelled";

            return ProgressId.ToString();
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
                return RedirectToAction("Index", "Task");
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

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetFromJsonAsync<TaskDto>($"https://localhost:7148/api/Task/{id.ToString()}");
            if (response is not null)
            {
                return View(response);
            }
            return View(null);
        }
    }
}
