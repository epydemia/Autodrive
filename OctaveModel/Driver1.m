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
## @deftypefn {} {@var{retval} =} Driver1 (@var{input1}, @var{input2})
##
## @seealso{}
## @end deftypefn

## Author: Daniele <Daniele@PROBOOK>
## Created: 2020-11-08


function retval=Driver1(DistanceToObstacle)
  GainAcc=0.015;
  GainBrk=-200;
  cmdACC=max(0,min(1,DistanceToObstacle*GainAcc));
  if abs(DistanceToObstacle<1e-3)
    cmdBrk=-1;
  else
    cmdBrk=min(0,max(-1,1/DistanceToObstacle*GainBrk));
  endif
  
  retval=cmdACC+cmdBrk;
endfunction