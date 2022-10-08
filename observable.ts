class Subject<T>{
    private state$:T | any;
    private callbacks:any[];
    constructor(_state?:T){
        this.state$=_state || undefined;
        this.callbacks=[];
    }
    subscribe(callback:(value:T | any)=>void){
        this.callbacks.push(callback);
    }
    next(_value:T){
        this.state$=_value;
        this.callbacks.forEach(call=>{
            call(this.state$);
        })
    }
}
class Observable<T>{
    private state$:T | any;
    private callback:(res:any)=>void;
    constructor(_state?:T){
        this.state$=_state || undefined;
        this.callback=()=>0;
    }
    subscribe(callback:(value:T | any)=>void){
        this.callback=callback;
    }
    next(_value:T){
        this.state$=_value;
        this.callback(this.state$);
    }
    map(callback:(value:T)=>any):T{
        return callback(this.state$);
    }
}
