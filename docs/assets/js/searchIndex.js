
var camelCaseTokenizer = function (builder) {

  var pipelineFunction = function (token) {
    var previous = '';
    // split camelCaseString to on each word and combined words
    // e.g. camelCaseTokenizer -> ['camel', 'case', 'camelcase', 'tokenizer', 'camelcasetokenizer']
    var tokenStrings = token.toString().trim().split(/[\s\-]+|(?=[A-Z])/).reduce(function(acc, cur) {
      var current = cur.toLowerCase();
      if (acc.length === 0) {
        previous = current;
        return acc.concat(current);
      }
      previous = previous.concat(current);
      return acc.concat([current, previous]);
    }, []);

    // return token for each string
    // will copy any metadata on input token
    return tokenStrings.map(function(tokenString) {
      return token.clone(function(str) {
        return tokenString;
      })
    });
  }

  lunr.Pipeline.registerFunction(pipelineFunction, 'camelCaseTokenizer')

  builder.pipeline.before(lunr.stemmer, pipelineFunction)
}
var searchModule = function() {
    var documents = [];
    var idMap = [];
    function a(a,b) { 
        documents.push(a);
        idMap.push(b); 
    }

    a(
        {
            id:0,
            title:"EntityComponentCollectionExtensions",
            content:"EntityComponentCollectionExtensions",
            description:'',
            tags:''
        },
        {
            url:'/XenkoToolkit/api/XenkoToolkit.Engine/EntityComponentCollectionExtensions',
            title:"EntityComponentCollectionExtensions",
            description:""
        }
    );
    a(
        {
            id:1,
            title:"EntityExtensions",
            content:"EntityExtensions",
            description:'',
            tags:''
        },
        {
            url:'/XenkoToolkit/api/XenkoToolkit.Engine/EntityExtensions',
            title:"EntityExtensions",
            description:""
        }
    );
    a(
        {
            id:2,
            title:"TransformExtensions",
            content:"TransformExtensions",
            description:'',
            tags:''
        },
        {
            url:'/XenkoToolkit/api/XenkoToolkit.Engine/TransformExtensions',
            title:"TransformExtensions",
            description:""
        }
    );
    a(
        {
            id:3,
            title:"EasingFunction",
            content:"EasingFunction",
            description:'',
            tags:''
        },
        {
            url:'/XenkoToolkit/api/XenkoToolkit.Mathematics/EasingFunction',
            title:"EasingFunction",
            description:""
        }
    );
    a(
        {
            id:4,
            title:"GameExtensions",
            content:"GameExtensions",
            description:'',
            tags:''
        },
        {
            url:'/XenkoToolkit/api/XenkoToolkit.Engine/GameExtensions',
            title:"GameExtensions",
            description:""
        }
    );
    a(
        {
            id:5,
            title:"MathUtilEx",
            content:"MathUtilEx",
            description:'',
            tags:''
        },
        {
            url:'/XenkoToolkit/api/XenkoToolkit.Mathematics/MathUtilEx',
            title:"MathUtilEx",
            description:""
        }
    );
    a(
        {
            id:6,
            title:"RaySegment",
            content:"RaySegment",
            description:'',
            tags:''
        },
        {
            url:'/XenkoToolkit/api/XenkoToolkit.Mathematics/RaySegment',
            title:"RaySegment",
            description:""
        }
    );
    a(
        {
            id:7,
            title:"DictionaryExtensions",
            content:"DictionaryExtensions",
            description:'',
            tags:''
        },
        {
            url:'/XenkoToolkit/api/XenkoToolkit.Collections/DictionaryExtensions',
            title:"DictionaryExtensions",
            description:""
        }
    );
    a(
        {
            id:8,
            title:"Easing",
            content:"Easing",
            description:'',
            tags:''
        },
        {
            url:'/XenkoToolkit/api/XenkoToolkit.Mathematics/Easing',
            title:"Easing",
            description:""
        }
    );
    a(
        {
            id:9,
            title:"RandomListExtensions",
            content:"RandomListExtensions",
            description:'',
            tags:''
        },
        {
            url:'/XenkoToolkit/api/XenkoToolkit.Collections/RandomListExtensions',
            title:"RandomListExtensions",
            description:""
        }
    );
    a(
        {
            id:10,
            title:"EntityComponentSearchExtensions",
            content:"EntityComponentSearchExtensions",
            description:'',
            tags:''
        },
        {
            url:'/XenkoToolkit/api/XenkoToolkit.Engine/EntityComponentSearchExtensions',
            title:"EntityComponentSearchExtensions",
            description:""
        }
    );
    a(
        {
            id:11,
            title:"CollectionExtensions",
            content:"CollectionExtensions",
            description:'',
            tags:''
        },
        {
            url:'/XenkoToolkit/api/XenkoToolkit.Collections/CollectionExtensions',
            title:"CollectionExtensions",
            description:""
        }
    );
    a(
        {
            id:12,
            title:"MaterialExtensions",
            content:"MaterialExtensions",
            description:'',
            tags:''
        },
        {
            url:'/XenkoToolkit/api/XenkoToolkit.Rendering/MaterialExtensions',
            title:"MaterialExtensions",
            description:""
        }
    );
    a(
        {
            id:13,
            title:"RandomExtensions",
            content:"RandomExtensions",
            description:'',
            tags:''
        },
        {
            url:'/XenkoToolkit/api/XenkoToolkit.Mathematics/RandomExtensions',
            title:"RandomExtensions",
            description:""
        }
    );
    a(
        {
            id:14,
            title:"SimulationExtensions",
            content:"SimulationExtensions",
            description:'',
            tags:''
        },
        {
            url:'/XenkoToolkit/api/XenkoToolkit.Physics/SimulationExtensions',
            title:"SimulationExtensions",
            description:""
        }
    );
    a(
        {
            id:15,
            title:"CameraExtensions",
            content:"CameraExtensions",
            description:'',
            tags:''
        },
        {
            url:'/XenkoToolkit/api/XenkoToolkit.Engine/CameraExtensions',
            title:"CameraExtensions",
            description:""
        }
    );
    a(
        {
            id:16,
            title:"Space",
            content:"Space",
            description:'',
            tags:''
        },
        {
            url:'/XenkoToolkit/api/XenkoToolkit.Engine/Space',
            title:"Space",
            description:""
        }
    );
    a(
        {
            id:17,
            title:"EnumerableExtensions",
            content:"EnumerableExtensions",
            description:'',
            tags:''
        },
        {
            url:'/XenkoToolkit/api/XenkoToolkit.Collections/EnumerableExtensions',
            title:"EnumerableExtensions",
            description:""
        }
    );
    a(
        {
            id:18,
            title:"ScriptSystemExtensions",
            content:"ScriptSystemExtensions",
            description:'',
            tags:''
        },
        {
            url:'/XenkoToolkit/api/XenkoToolkit.Engine/ScriptSystemExtensions',
            title:"ScriptSystemExtensions",
            description:""
        }
    );
    a(
        {
            id:19,
            title:"ListStackExtensions",
            content:"ListStackExtensions",
            description:'',
            tags:''
        },
        {
            url:'/XenkoToolkit/api/XenkoToolkit.Collections/ListStackExtensions',
            title:"ListStackExtensions",
            description:""
        }
    );
    a(
        {
            id:20,
            title:"PrefabExtensions",
            content:"PrefabExtensions",
            description:'',
            tags:''
        },
        {
            url:'/XenkoToolkit/api/XenkoToolkit.Engine/PrefabExtensions',
            title:"PrefabExtensions",
            description:""
        }
    );
    a(
        {
            id:21,
            title:"ModelComponentExtensions",
            content:"ModelComponentExtensions",
            description:'',
            tags:''
        },
        {
            url:'/XenkoToolkit/api/XenkoToolkit.Engine/ModelComponentExtensions',
            title:"ModelComponentExtensions",
            description:""
        }
    );
    var idx = lunr(function() {
        this.field('title');
        this.field('content');
        this.field('description');
        this.field('tags');
        this.ref('id');
        this.use(camelCaseTokenizer);

        this.pipeline.remove(lunr.stopWordFilter);
        this.pipeline.remove(lunr.stemmer);
        documents.forEach(function (doc) { this.add(doc) }, this)
    });

    return {
        search: function(q) {
            return idx.search(q).map(function(i) {
                return idMap[i.ref];
            });
        }
    };
}();
