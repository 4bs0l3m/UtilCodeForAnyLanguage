export class CacheService{
  cacheName="cache";
  cacheSplitOp=':';
  cacheItems:{key:string,value:string}[]=[];
  setCacheItem(id:number,key:string,value:string){
    localStorage.setItem(key+":"+id.toString(),value);
  }
  getCacheItemByID(id:number,key:string):string{
    return localStorage.getItem(key+":"+id.toString());
  }
  compareCacheItem(CurrentId:number,CurrentKey:string,value:string):boolean{
    return localStorage.getItem(CurrentKey+":"+CurrentId.toString())==value;
  }

  getAllCacheByKey(key:string):any[]{
    this.cacheItems=[];
    for (let i = 0; i < localStorage.length; i++) {
      if(localStorage.key(i).split(":")[0]==key){
        this.cacheItems.push({key:localStorage.key(i),value:localStorage.getItem(localStorage.key(i))});
      }
    }
    return this.cacheItems;
  }
  clearCacheByKey(key:string){
    for (let i = 0; i < localStorage.length; i++) {
      if(localStorage.key(i).split(":")[0]==key){
        localStorage.removeItem(localStorage.key(i));
      }
    }
  }
  clearCache(){
    localStorage.clear();
  }
  cacheKeyGenerator(key:string,id:number):string{
    let keyStringArr:string[]=[];
    let keyString:string="";
    keyStringArr.push(this.cacheName);
    keyStringArr.push(key);
    keyStringArr.push(id.toString());
    for (let index = 0; index < keyStringArr.length-1; index++) {
      keyString += keyStringArr[index]+this.cacheSplitOp;
    }
    
    keyString += keyStringArr[keyStringArr.length-1];
    return keyString;
  }
}
