import { Pipe, PipeTransform } from "@angular/core";
/*
 * Returns true if list length is even or the current index is not last index
 * Usage:
 *   index | displayEven:length
 * Example:
 *   {{ 2 | displayEven:10 }}
 *   formats to: true
 */
@Pipe({
  name: "displayEven"
})
export class DisplayEvenPipe implements PipeTransform {
  transform(index: number, length: number): boolean {
    return length % 2 === 0 || index < length - 1;
  }
}
