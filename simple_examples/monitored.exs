defmodule Demo.Monitor do
  def do_work do
  	IO.puts "doing work..."
    # Process.sleep(6000)
  	exit(:error)
	end
end

spawn_monitor Demo.Monitor, :do_work, []

receive do
	msg -> IO.puts "The monitored process says: #{inspect(msg)}"
	after 5000 -> IO.puts "The monitored process says nothing."
end

# Process.sleep(1000)
IO.puts "Hello from Main"
