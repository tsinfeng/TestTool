OutputLine("交流信号机电流：")
YPList={8.5,9.5,9.0,11.0,12.5,13.5,15.0,16.5,17.5,20.0,21.5,22.5,23.5,24.5,26.0} 
UM71List={10.3,11.4,12.5,13.6,14.7,15.8,16.9,18.0,19.1,20.2,22.4,24.6,26.8,29} 

while true do

--[[UM71 send signal]]
for carrier=1700,2600,300 do
for i=1,#UM71List do
for j=100,400,50 do
--[[FMSignal(1,carrier,UM71List[i],11,j*0.001)]]
   local out_str=string.format("UM71  %d %f %d %d", carrier,UM71List[i], i, j)
   OutputLine(out_str)
   Sleep(5000)
end
end
end

--[[Fre send signal]]
for carrier=550,850,100 do
for i=1,#YPList do
for j=100,400,50 do
--[[FMSignal(1,carrier,YPList[i],55,j*0.001)]]
   local out_str1 = string.format("Frequency %d, %f, %d", carrier, YPList[i], j)
   OutputLine(out_str1)
   Sleep(5000)
end
end
end

end