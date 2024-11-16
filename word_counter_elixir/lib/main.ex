defmodule Main do
  use Application

  def start(_type, _args) do
    word = "now"
    file_path = "YOUR_PATH"
    iterations = 7
    # processes = System.schedulers_online() - 1
    processes = System.schedulers_online() - 1
    case File.read(file_path) do
      {:ok, content} ->
        action = fn -> WordCounter.count_word_in_file(content, word, processes) end
        {min_time, final_result } = TimeEstimator.measure_execution_time(action, iterations)
        IO.puts("The word '#{word}' appears #{final_result} times in the file.")
        IO.puts("The minimum time to execute the action was #{min_time / 1_000_000} seconds.")
      {:error, reason} ->
        IO.puts("Failed to read the file: #{reason}")
    end


    Task.start(fn -> :timer.sleep(1000); IO.puts("done sleeping") end)
  end

end
