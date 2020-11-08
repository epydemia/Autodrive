## Copyright (C) 2020 Daniele
## 
## This program is free software: you can redistribute it and/or modify it
## under the terms of the GNU General Public License as published by
## the Free Software Foundation, either version 3 of the License, or
## (at your option) any later version.
## 
## This program is distributed in the hope that it will be useful, but
## WITHOUT ANY WARRANTY; without even the implied warranty of
## MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
## GNU General Public License for more details.
## 
## You should have received a copy of the GNU General Public License
## along with this program.  If not, see
## <https://www.gnu.org/licenses/>.

## -*- texinfo -*- 
## @deftypefn {} {@var{retval} =} Autodrive (@var{input1}, @var{input2})
##
## @seealso{}
## @end deftypefn

## Author: Daniele <Daniele@PROBOOK>
## Created: 2020-11-08

Heading = 0;
Speed = 0;
PositionX = 0;
PositionY = 0;
Acceleration = 0;
MaxSpeed=56;
AccelerationMax=2.8;
BrakeMax=14;
Cx = AccelerationMax / (MaxSpeed * MaxSpeed);
ObstaclePosition=1000;

dt=0.1;
t=[0:dt:100];
PosVector=zeros(1,length(t));
SpeedVector=zeros(1,length(t));
CmdVector=zeros(1,length(t));

for i=1:length(t)
  distanceToObstacle=ObstaclePosition-PositionX;
  command=Driver1(distanceToObstacle);
  cmdVector(i)=command;
  if(command>0)
    AccCMD=command;
    BrkCMD=0;
  else
    AccCMD=0;
    BrkCMD=command*-1;
  endif
  
  Friction=Cx*Speed*Speed;
  Acceleration=AccelerationMax*AccCMD-BrakeMax*BrkCMD-Friction;
  Speed=max(0,Speed+Acceleration*dt);
  PositionX=PositionX+cos(Heading)*Speed*dt;
  SpeedVector(i)=Speed;
  PosVector(i)=PositionX;
 endfor
figure(1);
 subplot(3,1,1)
 plot(t,SpeedVector);
 subplot(3,1,2)
 plot(t,PosVector);
  subplot(3,1,3)
 plot(t,cmdVector);
 



   




