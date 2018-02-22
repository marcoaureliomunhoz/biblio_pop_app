/*!
 * Framework JavaScript v0.5
 * http://github.com/marcoaureliomunhoz
 * 
 * Author: Marco Aurélio Munhóz
 *
 * License: MIT
 *
 * Date: 2018-02-21
 */
(function() {
	js_frame = {};
	//core functions
	js_frame.httpRequest = function(url, type, data, contentType, dataType, beforeSend, ifSucess, refreshOnSucess, ifError, refreshOnError) {
		$.ajax({
          url  : url,
          type : type,
		  data : data,
		  contentType : contentType, 
		  dataType : dataType, 
          beforeSend : beforeSend
     	})
     	.done(function(response){
          	ifSucess(response);
          	if (refreshOnSucess) {
          		js_frame.refresh();
          	}
     	})
     	.fail(function(jqXHR, textStatus, response){
     		ifError(jqXHR, textStatus, response);
     		if (refreshOnError) {
     			js_frame.refresh();
     		}
     	}); 
	};
	js_frame.httpGet = function(url, beforeSend, ifSucess, refreshOnSucess, ifError, refreshOnError) {
		$.ajax({
          url  : url,
          type : 'GET',
          beforeSend : beforeSend
     	})
     	.done(function(response){
          	ifSucess(response);
          	if (refreshOnSucess) {
          		js_frame.refresh();
          	}
     	})
     	.fail(function(jqXHR, textStatus, response){
     		ifError(jqXHR, textStatus, response);
     		if (refreshOnError) {
     			js_frame.refresh();
     		}
     	}); 
	};
	js_frame.httpPost = function(url, data, contentType, dataType, beforeSend, ifSucess, refreshOnSucess, ifError, refreshOnError) {
		$.ajax({
          url  : url,
          type : 'POST',
		  data : data,
		  contentType : contentType, 
		  dataType : dataType, 
          beforeSend : beforeSend
     	})
     	.done(function(response){
          	ifSucess(response);
          	if (refreshOnSucess) {
          		js_frame.refresh();
          	}
     	})
     	.fail(function(jqXHR, textStatus, response){
     		ifError(jqXHR, textStatus, response);
     		if (refreshOnError) {
     			js_frame.refresh();
     		}
     	}); 
	};
	js_frame.httpPut = function(url, data, contentType, dataType, beforeSend, ifSucess, refreshOnSucess, ifError, refreshOnError) {
		$.ajax({
          url  : url,
          type : 'PUT',
		  data : data,
		  contentType : contentType, 
		  dataType : dataType, 
          beforeSend : beforeSend
     	})
     	.done(function(response){
          	ifSucess(response);
          	if (refreshOnSucess) {
          		js_frame.refresh();
          	}
     	})
     	.fail(function(jqXHR, textStatus, response){
     		ifError(jqXHR, textStatus, response);
     		if (refreshOnError) {
     			js_frame.refresh();
     		}
     	}); 
	};
	js_frame.redirect = function(to, params) {
		var url = to;
		if (params && params.length>0) url += '?' + params;
		window.location.href = url;
	};
	js_frame.cloneObject = function(target) {
		return JSON.parse(JSON.stringify(target));
	};
	js_frame.cloneTarget = function(target) {
		if (typeof target == 'object') {
			return cloneObject(target);
		}
		return target;
	};
	js_frame.replaceAll = function(find, replace, str) {
      while(str.indexOf(find) > -1) {
        str = str.replace(find, replace);
      }
      return str;
    };
	js_frame.objectToArray = function(name, object, array) {			
		var objectType = typeof object;
		var objectInstanceArray = object instanceof Array;
		var objectInstanceFunction = object instanceof Function;
		if (objectType == 'object') {
			for (var property in object) {				
				var propertyType = typeof object[property];
				if (propertyType == 'function') {
				} else {
					if (propertyType == 'object') {
						var newName = name+'.'+property;
						js_frame.objectToArray(newName, object[property], array);
					} else {
						var finalName = name+'.'+property;
						if (array) array[finalName] = object[property];
					}
				}
			}
		} else {
			if (array) array.push(object);
		}
	};	
	js_frame.getUrlParams = function(url) {

		// get query string from url (optional) or window
		var queryString = url ? url.split('?')[1] : window.location.search.slice(1);
	  
		// we'll store the parameters here
		var obj = {};
	  
		// if query string exists
		if (queryString) {
	  
		  // stuff after # is not part of query string, so get rid of it
		  queryString = queryString.split('#')[0];
	  
		  // split our query string into its component parts
		  var arr = queryString.split('&');
	  
		  for (var i=0; i<arr.length; i++) {
			// separate the keys and the values
			var a = arr[i].split('=');
	  
			// in case params look like: list[]=thing1&list[]=thing2
			var paramNum = undefined;
			var paramName = a[0].replace(/\[\d*\]/, function(v) {
			  paramNum = v.slice(1,-1);
			  return '';
			});
	  
			// set parameter value (use 'true' if empty)
			var paramValue = typeof(a[1])==='undefined' ? true : a[1];
	  
			// (optional) keep case consistent
			paramName = paramName.toLowerCase();
			paramValue = paramValue.toLowerCase();
	  
			// if parameter name already exists
			if (obj[paramName]) {
			  // convert value to array (if still string)
			  if (typeof obj[paramName] === 'string') {
				obj[paramName] = [obj[paramName]];
			  }
			  // if no array index number specified...
			  if (typeof paramNum === 'undefined') {
				// put the value on the end of the array
				obj[paramName].push(paramValue);
			  }
			  // if array index number specified...
			  else {
				// put the value at that index number
				obj[paramName][paramNum] = paramValue;
			  }
			}
			// if param name doesn't exist yet, set it
			else {
			  obj[paramName] = paramValue;
			}
		  }
		}
	  
		return obj;
	};
	//framework functions
	js_frame.services = [];
	js_frame.newService = function(id, service) {
		this.services.push({
			id : id,
			service : service
		});
	};
	js_frame.getService = function(id) {
		var service = null;
		for (var i = 0; i < js_frame.services.length; i++) {
			if (js_frame.services[i].id == id) {
				service = new js_frame.services[i].service();
			}
		}
		return service;
	};
	js_frame.contexts = [];
	js_frame.newContext = function(id, context) {
		this.contexts.push({
			id : id,
			context : context
		});
	};
	js_frame.getContext = function(id) {
		var context = null;
		for (var i = 0; i < js_frame.contexts.length; i++) {
			if (js_frame.contexts[i].id == id) {
				var contextFunction = js_frame.contexts[i].context.toString();
				var par_ini = contextFunction.indexOf('(');
				var par_fin = contextFunction.indexOf(')');
				var contextParameters = contextFunction.substring(par_ini+1, par_fin).trim();
				var contextParameters = contextParameters.split(',');
				//console.log('contextParameters:', contextParameters);
				//console.log('services:', js_frame.services);
				if (contextParameters.length > 0) {
					for (var p = 0; p < contextParameters.length; p++) {
						var contextParameterName = contextParameters[p].trim();
						var service = js_frame.getService(contextParameterName);
						if (typeof service == 'object') {
							contextParameters[p] = service;
						}
					}
					//console.log('contextParameters:', contextParameters);
					context = {};
					js_frame.contexts[i].context.apply(context, contextParameters);
				} else {
					context = new js_frame.contexts[i].context();
				}
				context.redirect = js_frame.redirect;
				context.getUrlParams = js_frame.getUrlParams;
			}
		}
		return context;
	};	
	js_frame.refresh = function() {
	};
	js_frame.init = function() {
		var js_ctx_elem = $('[js_ctx]');
		if (js_ctx_elem.length == 1) {
			var js_ctx = js_frame.getContext(js_ctx_elem.attr('js_ctx'));
			if (!js_ctx) return;
			
			if (js_ctx['onCreate'] instanceof Function) {
				js_ctx.onCreate();
			}
		
			var js_ctx_aux = {};
			var updateJsCtxAux = function() {
				var attr_is_function = false;
				var attr_is_array = false;
				var attr_is_object = false;
				for (var pname in js_ctx) {
					attr_is_function = typeof js_ctx[pname] == 'function';
					attr_is_array = typeof js_ctx[pname] == 'array';
					attr_is_object = typeof js_ctx[pname] == 'object';
					if (!attr_is_function && !attr_is_array) {
						if (attr_is_object) {
							js_ctx_aux[pname] = js_frame.cloneObject(js_ctx[pname]);
						} else {
							js_ctx_aux[pname] = js_ctx[pname];
						}
					}
				}
			};
			updateJsCtxAux();

			var JsForeachMetadata = function(id, item, ctx, propListName, template) {
				this.id = id;
				this.item = item;
				//this.list = list;
				this.ctx = ctx;
				this.propListName = propListName;
				this.template = template;
				this.hash = '';
				this.getList = function() {
					//console.log('getList', this.ctx, this.propListName);
					//var ctx_array = [];
					//js_frame.objectToArray('ctx',this.ctx,ctx_array);
					//console.log(ctx_array);
					var propListNameArray = this.propListName.split('.');
					//console.log(propListNameArray);					
					if (propListNameArray.length>1) {
						var prop_ret = this.ctx[propListNameArray[0]];
						for (var p = 1; p < propListNameArray.length; p++) {
							prop_ret = prop_ret[propListNameArray[p]];
						}
						return prop_ret;
					} 
					return this.ctx[this.propListName];
					//return ctx_array['ctx.'+this.propListName];
				};
				this.changed = function() {
					//var new_hash = JSON.stringify(this.list);
					var new_hash = JSON.stringify(this.getList());	
					//console.log(new_hash); 
					if (new_hash != this.hash) {
						this.hash = new_hash;
						return true;
					}
					return false;
				};
			};
			var js_foreach_list = [];

			js_ctx.visible = function(id) {
				var nid = id.substring(1,id.length-1);
				var elem = $('#'+nid);
				if (elem.length>0) {					
					elem.show();
				}
			};
			js_ctx.invisible = function(id) {
				var nid = id.substring(1,id.length-1);
				var elem = $('#'+nid);
				if (elem.length>0) {
					elem.hide();
				}
			};

			var propagateChangesProperties = function(vjs_ctx_aux, vjs_ctx, js_prop) {
				//console.log('js_prop:', js_prop, 'ctx: ', vjs_ctx, 'ctx_aux: ', vjs_ctx_aux);
				for (var js_ctx_aux_prop_name in vjs_ctx_aux) {
					//console.log('typeof: ',typeof vjs_ctx_aux[js_ctx_aux_prop_name], ' | name: ', js_ctx_aux_prop_name);
					var is_array = vjs_ctx_aux[js_ctx_aux_prop_name] instanceof Array;
					var is_object = vjs_ctx_aux[js_ctx_aux_prop_name] instanceof Object;
					//console.log('instanceof array: ', is_array);
					//console.log('instanceof object: ', is_object);
					if (is_array) {
					} else {
						//if (typeof vjs_ctx_aux[js_ctx_aux_prop_name] == 'object') {
						if (is_object) {
							var new_js_prop = js_prop + (js_prop.length>0?'.':'') + js_ctx_aux_prop_name;
							//console.log('object new_js_prop',new_js_prop);
							propagateChangesProperties(vjs_ctx_aux[js_ctx_aux_prop_name], vjs_ctx[js_ctx_aux_prop_name], new_js_prop);
						} else {			
							var new_val = vjs_ctx[js_ctx_aux_prop_name];						
							var cur_val = vjs_ctx_aux[js_ctx_aux_prop_name];
							if (cur_val !== new_val) {
								var new_js_prop = js_prop + (js_prop.length>0?'.':'') + js_ctx_aux_prop_name;
								vjs_ctx_aux[js_ctx_aux_prop_name] = new_val;
								$('*[js_prop="'+new_js_prop+'"]').each(function(index){
									$(this).val(new_val);
								});
								$('*[js_prop_view="'+new_js_prop+'"]').each(function(index){
									$(this).text(new_val);
								});
							} 
						}
					}
				}
			};

			var setElementNodeValue = function(list_item, js_prop_for_value_vet, vet_i, elem_node, elem_node_type) {
				for (var prop_item in list_item) {
					if (prop_item == js_prop_for_value_vet[vet_i]) {
						if (typeof list_item[prop_item] == 'object') {
							setElementNodeValue(list_item[prop_item], js_prop_for_value_vet, ++vet_i, elem_node, elem_node_type);								
						} else {
							if (elem_node_type == 'input') {
								elem_node.val(list_item[prop_item]);
							} else {
								elem_node.html(list_item[prop_item]);
							}
						}
					}
				}
			};

			var configJsKeyUp = function(elem) {
				var js_event_array = elem.attr('js_keyup').split(';');

				elem.keyup(function(event){
					updateJsCtxAux();

					for (var i=0; i<js_event_array.length; i++) {
						var js_event = js_event_array[i].trim();
						if (js_event.length>=3) {
							var par_ini = js_event.indexOf('(');
							var par_fin = js_event.indexOf(')');
							var args = js_event.substring(par_ini+1, par_fin).trim();
							var js_event_name = js_event.substring(0, par_ini);

							if (args.length>0) {
								js_ctx[js_event_name].apply(js_ctx, args.split(','));
							} else {
								js_ctx[js_event_name]();
							}
						}
					}

					propagateChanges();
				});
			};

			var configJsSelectChange = function(elem) {
				var js_event_array = elem.attr('js_select').split(';');

				elem.change(function(event){
					updateJsCtxAux();

					for (var i=0; i<js_event_array.length; i++) {
						var js_event = js_event_array[i].trim();
						if (js_event.length>=3) {
							var par_ini = js_event.indexOf('(');
							var par_fin = js_event.indexOf(')');
							var args = js_event.substring(par_ini+1, par_fin).trim();
							var js_event_name = js_event.substring(0, par_ini);

							if (args.length>0) {
								js_ctx[js_event_name].apply(js_ctx, args.split(','));
							} else {
								js_ctx[js_event_name]();
							}
						}
					}

					propagateChanges();
				});
			};

			var configJsClick = function(elem) {
				var js_event_array = elem.attr('js_click').split(';');

				elem.click(function(event){
					updateJsCtxAux();

					for (var i=0; i<js_event_array.length; i++) {
						var js_event = js_event_array[i].trim();
						if (js_event.length>=3) {
							var par_ini = js_event.indexOf('(');
							var par_fin = js_event.indexOf(')');
							var args = js_event.substring(par_ini+1, par_fin).trim();
							var js_event_name = js_event.substring(0, par_ini);
							if (js_ctx[js_event_name]) {
								if (args.length>0) {
									js_ctx[js_event_name].apply(js_ctx, args.split(','));
								} else {
									js_ctx[js_event_name]();
								}
							} else {
								console.log(`FrameJS: event function "${js_event_name}" not found in context.`);
							}
						}
					}

					propagateChanges();
				});
			};

			var propagateChangesForeachItem = function(js_foreach_item) {
				//console.log(js_foreach_item);
				//console.log(js_foreach_item.id);
				var js_foreach_elem = $('[js_foreach_id="'+js_foreach_item.id+'"]');
				js_foreach_elem.empty();
				//var template = js_foreach_item.template;
				//console.log(template);
				//var list = js_foreach_item.list;
				var list = js_foreach_item.getList();
				//console.log(list);
				if (!list) return;
				var list_length = list.length;
				//aqui percorre a lista
				for (var i = 0; i < list_length; i++) {
					var template = js_foreach_item.template;
					//console.log(list[i]);
					var listItemArray = [];
					js_frame.objectToArray(js_foreach_item.item, list[i], listItemArray);
					//console.log(listItemArray);
					for (var propListItemArray in listItemArray) {
						var find = '{{'+propListItemArray+'}}';
						var replace = undefined;
						var typePropListItemArray = typeof listItemArray[propListItemArray];
						//console.log(typePropListItemArray);
						replace = listItemArray[propListItemArray];
						//if (typePropListItemArray == 'string') replace = '"' + replace + '"';
						template = js_frame.replaceAll(find, replace, template);
					}
					//console.log(template);
					var elem_root = $('<js_root_for></js_root_for>');
					elem_root.append(template);					
					//para cada item da lista
					//percorre os elementos com js_prop_for
					elem_root.find('[js_prop_for]').each(function(){
						var elem_node = $(this);
						var js_prop_for_value = elem_node.attr('js_prop_for');
						var elem_node_type = elem_node.prop('tagName').toLowerCase();
						if (js_prop_for_value == js_foreach_item.item) {
							if (elem_node_type == 'input') {
								elem_node.val(list[i]);
							} else {
								elem_node.html(list[i]);
							}
						} else {							
							var js_prop_for_value_vet = js_prop_for_value.split('.');
							if (js_prop_for_value_vet instanceof Array && js_prop_for_value_vet.length > 1) {
								if (js_prop_for_value_vet[0] == js_foreach_item.item) { 
									setElementNodeValue(list[i], js_prop_for_value_vet, 1, elem_node, elem_node_type);
								}
							} else {
								if (elem_node_type == 'input') {
									elem_node.val(js_ctx[js_prop_for_value]);
								} else {
									elem_node.html(js_ctx[js_prop_for_value]);
								}
							}
						}
						elem_node.removeAttr('js_prop_for');
					});
					js_foreach_elem.append(elem_root.html());
				}
				js_foreach_elem.find('[js_click]').each(function(index){					
					configJsClick($(this));
				});
			};

			var propagateChangesForeachList = function() {
				for (var i = js_foreach_list.length-1; i >= 0 ; i--) {
					var item_changed = js_foreach_list[i].changed();
					if (item_changed) {
						propagateChangesForeachItem(js_foreach_list[i]);
					}
				}
			};

			var propagateChanges = function() {
				//console.log('propagateChanges');
				//console.log('ctx_aux ==> ', js_ctx_aux);
				//console.log('ctx ==> ', js_ctx);
				propagateChangesProperties(js_ctx_aux, js_ctx, '');
				propagateChangesForeachList();
			};

			var getValueJsProp = function(name) {
				var value = null;
				var posi = name.indexOf('.');
				if (posi>0) {
					var vname = name.split('.');
					var tname = vname.length;
					if (tname > 0) {
						value = js_ctx[vname[0]];
						if (typeof value == 'object') {
							var i = 1;
							while ((i < tname) && (typeof value == 'object')) {
								value = value[vname[i]];
								i++;
							}
						}
					}
				} else {
					value = js_ctx[name];
				}
				return value;
			};

			var setValueJsProp = function(name, value) {
				var posi = name.indexOf('.');
				if (posi>0) {
					var vname = name.split('.');
					var tname = vname.length;
					if (tname > 0) {						
						var ctx_var = js_ctx[vname[0]];
						var ctx_ant = ctx_var;
						if (typeof ctx_var == 'object') {
							var i = 1;
							while ((i < tname) && (typeof ctx_var == 'object')) {
								ctx_ant = ctx_var;
								ctx_var = ctx_var[vname[i]];
								i++;
							}
						}
						ctx_ant[vname[tname-1]] = value;
					}
				} else {
					js_ctx[name] = value;
				}
			};

			$('input[js_prop],select[js_prop]').each(function(index){
				var input = $(this);
				var js_prop = input.attr('js_prop');

				var prop_value = getValueJsProp(js_prop);
				input.val(prop_value);

				var view = $('*[js_prop_view="'+js_prop+'"]');
				view.text(prop_value);

				//console.log(input.prop('tagName'));
				var input_bind = 'change past keyup';
				var input_tagname = input.prop('tagName').toLowerCase();
				if (input_tagname == 'select') input_bind = 'change';
				input.bind(input_bind,function(event){
					var input = $(this);
					var js_prop = input.attr('js_prop');					
					var input_value = input.val();
					setValueJsProp(js_prop, input_value);
					var view = $('*[js_prop_view="'+js_prop+'"]');
					view.text(input_value);
				});			
			});

			var js_foreach_count = $('[js_foreach]').length;
			$('[js_foreach]').each(function(index){
				var vthis = $(this);
				var js_foreach_id = ''+js_foreach_list.length+'';
				vthis.attr('js_foreach_id', js_foreach_id);
				var js_foreach_prop_value = vthis.attr('js_foreach');
				var js_foreach_vet = js_foreach_prop_value.split('in');
				js_foreach_vet[0] = js_foreach_vet[0].trim();
				js_foreach_vet[1] = js_foreach_vet[1].trim();
				//var js_foreach_item = new JsForeachMetadata(js_foreach_id, js_foreach_vet[0], js_ctx[js_foreach_vet[1]], vthis.html(), js_ctx, js_foreach_vet[1]);
				var js_foreach_item = new JsForeachMetadata(js_foreach_id, js_foreach_vet[0], js_ctx, js_foreach_vet[1], vthis.html());
				js_foreach_list.push(js_foreach_item);
				if (index == js_foreach_count-1) {
					propagateChangesForeachList();
				}
			});

			$('[js_click]').each(function(index){
				configJsClick($(this));
			});

			$('[js_keyup]').each(function(index){
				configJsKeyUp($(this));
			});

			$('[js_select]').each(function(index){
				configJsSelectChange($(this));
			});

			$('[js_attr]').each(function(index){
				var input = $(this);
				var js_attr = input.attr('js_attr');
				var js_attr_array = js_attr.split('=');
				//console.log(js_attr_array);
				var prop_value = getValueJsProp(js_attr_array[1]);
				input.attr(js_attr_array[0], prop_value);
				//console.log(prop_value);
			});

			if (js_ctx['onCreated'] instanceof Function) {
				js_ctx.onCreated();
			}

			js_frame.refresh = function() {
				//console.log('refresh');
				propagateChanges();
			};
		}
	};	
})();