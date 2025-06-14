using Xunit;

// Отключить Параллелизм при прогонке тестов
[assembly: CollectionBehavior(DisableTestParallelization = true)]

// Задать приортитетную очередь тестов
[assembly: TestCollectionOrderer("Airbnb.UserManagement.IntegrationTestings.PriorityOrderer", "Airbnb.UserManagement.IntegrationTestings")]