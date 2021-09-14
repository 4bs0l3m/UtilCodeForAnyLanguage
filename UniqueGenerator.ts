export class UniqueGenerator{
    static createID(){
         return Math.random().toString(36).substring(2,8+2);
     } 
     static newGuid() {
        return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
          var r = Math.random() * 16 | 0,
          v = c == 'x' ? r : (r & 0x3 | 0x8);
          return v.toString(16);
        });
      }
     
}
