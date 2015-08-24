OutputLine("交流信号机电流:")
while true do

--[[ send signal]]
for ampl=1,50,5 do
   --[[Single(1,50,ampl)]]
   local out_str=string.format("Alternating current signal module current: Fre,50 Ampl,%d", ampl)
   OutputLine(out_str)
   Sleep(5000)
end


end

