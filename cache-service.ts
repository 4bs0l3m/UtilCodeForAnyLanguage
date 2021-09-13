export class CacheService{
  cacheName="cache";
  cacheSplitOp='@';
  private cacheItems:{key:string,value:string}[]=[];
  setCacheItem(id:string,key:string,value:string){
    localStorage.setItem(this.cacheKeyGenerator(key,id),value);
  }
  getCacheItemByID(id:string,key:string):string{
    return localStorage.getItem(key+":"+id.toString());
  }
  compareCacheItem(CurrentId:number,CurrentKey:string,value:string):boolean{
    return localStorage.getItem(CurrentKey+this.cacheSplitOp+CurrentId.toString())==value;
  }
  getAllCacheByKey(key:string):any[]{
    this.cacheItems=[];
    for (let i = 0; i < localStorage.length; i++) {
      if(localStorage.key(i).split(this.cacheSplitOp)[0]==key){
        this.cacheItems.push({key:localStorage.key(i),value:localStorage.getItem(localStorage.key(i))});
      }
    }
    return this.cacheItems;
  }
  clearCache(key:string){
    for (let i = 0; i < localStorage.length; i++) {
      if(localStorage.key(i).split(this.cacheSplitOp)[0]==key){
        localStorage.removeItem(localStorage.key(i));
      }
    }
  }

  deleteItem(key:string,id:string):boolean{
    let fullKey:string=this.cacheKeyGenerator(key,id);
    if(localStorage.getItem(fullKey)){
      localStorage.removeItem(fullKey);
      return true;
    }else{
      return false;
    }
  }
  cacheKeyGenerator(key:string,id:string):string{
    let keyStringArr:string[]=[];
    let keyString:string="";//order: [cache-name]:[key]:[id]
    keyStringArr.push(this.cacheName);
    keyStringArr.push(key);
    keyStringArr.push(id.toString());
    for (let index = 0; index < keyStringArr.length-1; index++) {
      keyString += keyStringArr[index]+this.cacheSplitOp;//generate key with split operator
    }
    keyString += keyStringArr[keyStringArr.length-1];//for last item
    return keyString;
  }
}
