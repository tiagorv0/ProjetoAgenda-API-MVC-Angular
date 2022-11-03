import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'dataPropertyGetter',
  pure: false
})
export class DataPropertyGetterPipe implements PipeTransform {

  transform(object: any, keyName: string, ...args: unknown[]): unknown {
      let result = object[keyName];
      if(result['name']){
        return result['name'];
      }
      return result;
  }

}
