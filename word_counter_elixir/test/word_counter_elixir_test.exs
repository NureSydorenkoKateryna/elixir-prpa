defmodule WordCounterTest do
  use ExUnit.Case
  doctest WordCounter

  test "word count about" do
    file_path = "YOUR_PATH"
    assert WordCounter.count_word_in_file(file_path, "about") == 1011
  end

  test "word count now" do
    file_path = "YOUR_PATH"
    assert WordCounter.count_word_in_file(file_path, "now") == 1333
  end
end
