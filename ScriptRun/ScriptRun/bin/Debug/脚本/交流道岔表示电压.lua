OutputLine("交流道岔表示电压")
while true do

--[[ send signal]]
for ampl=1,300,20 do
   --[[Single(1,50,ampl)]]
   local out_str=string.format("Alternating current turnout module volatage: Fre,50 Ampl,%d", ampl)
   OutputLine(out_str)
   Sleep(5000)
end


end