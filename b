public async Task UpdatePartitionKeyAsync<T>(List<T> itemsToUpdate, Func<T, string> partitionKeySelector, Func<T, T> transformFunc) where T : class
{
    // Instantiate your generic repository
    var repository = new GenericRepository<T>(cosmosClient, databaseId, containerId);

    // Create and delete tasks lists
    var createTasks = new List<Task>();
    var deleteTasks = new List<Task>();

    foreach (var item in itemsToUpdate)
    {
        // Generate new item with updated partition key
        T newItem = transformFunc(item);

        // Schedule create operation
        createTasks.Add(repository.AddAsync(newItem));

        // Get the old partition key from the item
        string oldPartitionKey = partitionKeySelector(item);

        // Schedule delete operation for the old item
        // Assuming the repository.DeleteAsync method can take an id and a partition key
        deleteTasks.Add(repository.DeleteAsync(item.Id, oldPartitionKey));
    }

    // Run creation and deletion tasks
    await Task.WhenAll(createTasks);
    await Task.WhenAll(deleteTasks);
}