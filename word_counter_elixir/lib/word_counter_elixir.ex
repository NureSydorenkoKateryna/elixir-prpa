defmodule WordCounter do
  def count_word_in_file(content, word, processes) do
    count = count_word_count(content, word, processes)
    count
  end

  def count_word_count(text, word, processes) do
    lines = String.split(text, "\n", trim: true)
    chunk_size = max(div(length(lines), processes), 1)

    parent_pid = self()

    processes =
      lines
      |> Enum.chunk_every(chunk_size)
      |> Enum.map(fn chunk ->
        spawn(fn ->
          result = count_words_in_chunk(chunk, word)
          send(parent_pid, {self(), result})
        end)
      end)

    results =
      processes
      |> Enum.map(fn _pid ->
        receive do
          {_pid, result} -> result
        end
      end)

    Enum.sum(results)
  end

  defp count_words_in_chunk(lines_chunk, word) do
    word_pattern = ~r/\b#{Regex.escape(word)}\b/i

    lines_chunk
    |> Enum.flat_map(&String.split/1)
    |> Enum.filter(fn word_in_text ->
      Regex.match?(word_pattern, word_in_text)
    end)
    |> Enum.count()
  end
end
