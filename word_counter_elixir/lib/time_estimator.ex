defmodule TimeEstimator do
  def measure_execution_time(action, iterations) when is_function(action) do
    {min_time, final_result } = measure_execution_time_recursive(action, iterations, [])

    {min_time, final_result }
  end

  defp measure_execution_time_recursive(_action, 0, times_and_results), do:
    Enum.min_by(times_and_results, fn {time, _} -> time end)

  defp measure_execution_time_recursive(action, iterations_left, times_and_results) do
    {time, result} = measure_action_time(action)

    updated_times_and_results = [{time, result} | times_and_results]

    measure_execution_time_recursive(action, iterations_left - 1, updated_times_and_results)
  end

  defp measure_action_time(action) do
    {time, result} = :timer.tc(action) # returns result in microseconds
    {time, result}
  end
end
