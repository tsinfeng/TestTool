OutputLine("交流道岔动作电压")
while true do

--[[ send signal]]
for ampl=1,500,25 do
   --[[Single(1,50,ampl)]]
   local out_str=string.format("Alternating current turnout module volatage: Fre,50 Ampl,%d", ampl)
   OutputLine(out_str)
   Sleep(5000)
end


end