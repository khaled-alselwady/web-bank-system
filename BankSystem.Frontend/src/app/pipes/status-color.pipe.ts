import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'statusColor',
})
export class StatueColorPipe implements PipeTransform {
  transform(
    status: string,
    activeColor: string = 'blue',
    inactiveColor: string = 'gray'
  ): string {
    return status.toLowerCase() === 'active' ? activeColor : inactiveColor;
  }
}
