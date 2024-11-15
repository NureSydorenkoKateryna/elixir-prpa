defmodule Main do
  use Application

  def start(_type, _args) do
    IO.puts "starting"
    WordCounter.hello()
    Task.start(fn -> :timer.sleep(1000); IO.puts("done sleeping") end)
  end

end
