OutputLine("交流信号机电压:")
while true do

--[[ send signal]]
for ampl=1,20,2 do
   --[[Single(1,50,ampl)]]
   local out_str=string.format("Derect current signal module voltage: Fre,50 Ampl,%d", ampl)
   OutputLine(out_str)
   Sleep(5000)
end


end
