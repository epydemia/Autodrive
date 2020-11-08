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
## @deftypefn {} {@var{retval} =} CarPhisics (@var{input1}, @var{input2})
##
## @seealso{}
## @end deftypefn

## Author: Daniele <Daniele@PROBOOK>
## Created: 2020-11-08

function retval = CarPhisics (AccelerationCMD, BrakeCMD)
  Friction=Cx*Speed*Speed;
  Acceleration=AccelerationMax*AccelerationCMD-BrakeMax*BrakeCMD-Friction;
  Speed=Speed+Acceleration*dt;
  PositionX=cos(Heading)*Speed*dt;
  PositionY=sin(Heading)*Speed*dt;
endfunction
