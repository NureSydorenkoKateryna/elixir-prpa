# defmodule Demo.Linked do
#   def run(_caller) do
#     IO.puts "Linked.run"
#     # send caller, "Hello from Linked.run"
#     exit(:error)
#   end
# end

# Process.flag(:trap_exit, true)

# spawn_link Demo.Linked, :run, [self()]

# receive do
#   msg -> IO.puts "Received: #{inspect(msg)}"
# end


